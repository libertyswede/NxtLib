using System.Collections.Generic;
using System.Linq;

namespace NxtLib.Internal.LocalSign
{
    /// <summary>
    /// Ported from C to Java by Dmitry Skiba [sahn0], 23/02/08.
    /// Ported from Java to C# by LibertySwede, 26/06/14.
    /// Original: http://cds.xs4all.nl:8081/ecdh/
    /// 
    /// Generic 64-bit integer implementation of Curve25519 ECDH
    /// Written by Matthijs van Duin, 200608242056
    /// Public domain.
    /// 
    /// Based on work by Daniel J Bernstein, http://cr.yp.to/ecdh.html
    /// </summary>
    internal sealed class Curve25519
    {
        public const int KeySize = 32;
        public readonly byte[] Zero =
        {
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
            0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0
        };

        /* the prime 2^255-19 */
        public static readonly byte[] Prime =
        {
            237, 255, 255, 255,
            255, 255, 255, 255,
            255, 255, 255, 255,
            255, 255, 255, 255,
            255, 255, 255, 255,
            255, 255, 255, 255,
            255, 255, 255, 255,
            255, 255, 255, 127
        };

        /* group order (a prime near 2^252+2^124) */
        public static readonly byte[] Order =
        {
            237, 211, 245, 92,
            26, 99, 18, 88,
            214, 156, 247, 162,
            222, 249, 222, 20,
            0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 16
        };

        /********* KEY AGREEMENT *********/

        /// <summary>
        /// Private key clamping
        /// </summary>
        /// <param name="k">[in]  32 random bytes, [out] your private key for key agreement</param>
        public static void Clamp(byte[] k)
        {
            k[31] &= 0x7F;
            k[31] |= 0x40;
            k[0] &= 0xF8;
        }

        /// <summary>
        /// Key-pair generation
        /// s may be NULL if you don't care
        /// WARNING: if s is not NULL, this function has data-dependent timing
        /// </summary>
        /// <param name="p">[out] your public key</param>
        /// <param name="s">[out] your private key for signing</param>
        /// <param name="k">[in]  32 random bytes, [out] your private key for key agreement</param>
        public static void Keygen(byte[] p, byte[] s, byte[] k)
        {
            Clamp(k);
            Core(p, s, k, null);
        }

        /// <summary>
        /// Key agreement
        /// </summary>
        /// <param name="z">[out] shared secret (needs hashing before use)</param>
        /// <param name="k">[in]  your private key for key agreement</param>
        /// <param name="p">[in]  peer's public key</param>
        public static void Curve(byte[] z, byte[] k, byte[] p)
        {
            Core(z, null, k, p);
        }

        /********* DIGITAL SIGNATURES *********/

        /* deterministic EC-KCDSA
         *
         *    s is the private key for signing
         *    P is the corresponding public key
         *    Z is the context data (signer public key or certificate, etc)
         *
         * signing:
         *
         *    m = hash(Z, message)
         *    x = hash(m, s)
         *    keygen25519(Y, NULL, x);
         *    r = hash(Y);
         *    h = m XOR r
         *    sign25519(v, h, x, s);
         *
         *    output (v,r) as the signature
         *
         * verification:
         *
         *    m = hash(Z, message);
         *    h = m XOR r
         *    verify25519(Y, v, h, P)
         *
         *    confirm  r == hash(Y)
         *
         * It would seem to me that it would be simpler to have the signer directly do
         * h = hash(m, Y) and send that to the recipient instead of r, who can verify
         * the signature by checking h == hash(m, Y).  If there are any problems with
         * such a scheme, please let me know.
         *
         * Also, EC-KCDSA (like most DS algorithms) picks x random, which is a waste of
         * perfectly good entropy, but does allow Y to be calculated in advance of (or
         * parallel to) hashing the message.
         */

        /// <summary>
        /// Signature generation primitive, calculates (x-h)s mod q
        /// </summary>
        /// <param name="v">[out] signature value</param>
        /// <param name="h">[in]  signature hash (of message, signature pub key, and context data)</param>
        /// <param name="x">[in]  signature private key</param>
        /// <param name="s">[in]  private key for signing</param>
        /// <returns>true on success, false on failure (use different x or h)</returns>
        public static bool Sign(byte[] v, byte[] h, byte[] x, byte[] s)
        {
            // v = (x - h) s  mod q
            int w, i;
            var h1 = new byte[32];
            var x1 = new byte[32];
            var tmp1 = new byte[64];
            var tmp2 = new byte[64];

            // Don't clobber the arguments, be nice!
            Cpy32(h1, h);
            Cpy32(x1, x);

            // Reduce modulo group order
            var tmp3 = new byte[32];
            Divmod(tmp3, h1, 32, Order, 32);
            Divmod(tmp3, x1, 32, Order, 32);

            // v = x1 - h1
            // If v is negative, add the group order to it to become positive.
            // If v was already positive we don't have to worry about overflow
            // when adding the order because v < ORDER and 2*ORDER < 2^256
            mula_small(v, x1, 0, h1, 32, -1);
            mula_small(v, v, 0, Order, 32, 1);

            // tmp1 = (x-h)*s mod q
            Mula32(tmp1, v, s, 32, 1);
            Divmod(tmp2, tmp1, 64, Order, 32);

            for (w = 0, i = 0; i < 32; i++)
                w |= v[i] = tmp1[i];
            return w != 0;
        }

        /// <summary>
        /// Signature verification primitive, calculates Y = vP + hG
        /// </summary>
        /// <param name="y">[out] signature public key</param>
        /// <param name="v">[in]  signature value</param>
        /// <param name="h">[in]  signature hash</param>
        /// <param name="p">[in]  public key</param>
        public static void Verify(byte[] y, byte[] v, byte[] h, byte[] p)
        {
            /* Y = v abs(P) + h G  */
            var d = new byte[32];
            var q = new[] { new Long10(), new Long10() };
            var s = new[] { new Long10(), new Long10() };
            var yx = new[] { new Long10(), new Long10(), new Long10() };
            var yz = new[] { new Long10(), new Long10(), new Long10() };
            var t1 = new[] { new Long10(), new Long10(), new Long10() };
            var t2 = new[] { new Long10(), new Long10(), new Long10() };

            int vi = 0, hi = 0, di = 0, nvh = 0, i, k;

            /* set p[0] to G and p[1] to P  */

            Set(q[0], 9);
            Unpack(q[1], p);

            /* set s[0] to P+G and s[1] to P-G  */

            /* s[0] = (Py^2 + Gy^2 - 2 Py Gy)/(Px - Gx)^2 - Px - Gx - 486662  */
            /* s[1] = (Py^2 + Gy^2 + 2 Py Gy)/(Px - Gx)^2 - Px - Gx - 486662  */

            x_to_y2(t1[0], t2[0], q[1]); /* t2[0] = Py^2  */
            Sqrt(t1[0], t2[0]); /* t1[0] = Py or -Py  */
            var j = IsNegative(t1[0]);
            t2[0]._0 += 39420360; /* t2[0] = Py^2 + Gy^2  */
            Mul(t2[1], Base2Y, t1[0]); /* t2[1] = 2 Py Gy or -2 Py Gy  */
            Sub(t1[j], t2[0], t2[1]); /* t1[0] = Py^2 + Gy^2 - 2 Py Gy  */
            Add(t1[1 - j], t2[0], t2[1]); /* t1[1] = Py^2 + Gy^2 + 2 Py Gy  */
            Cpy(t2[0], q[1]); /* t2[0] = Px  */
            t2[0]._0 -= 9; /* t2[0] = Px - Gx  */
            Sqr(t2[1], t2[0]); /* t2[1] = (Px - Gx)^2  */
            Recip(t2[0], t2[1], 0); /* t2[0] = 1/(Px - Gx)^2  */
            Mul(s[0], t1[0], t2[0]); /* s[0] = t1[0]/(Px - Gx)^2  */
            Sub(s[0], s[0], q[1]); /* s[0] = t1[0]/(Px - Gx)^2 - Px  */
            s[0]._0 -= 9 + 486662; /* s[0] = X(P+G)  */
            Mul(s[1], t1[1], t2[0]); /* s[1] = t1[1]/(Px - Gx)^2  */
            Sub(s[1], s[1], q[1]); /* s[1] = t1[1]/(Px - Gx)^2 - Px  */
            s[1]._0 -= 9 + 486662; /* s[1] = X(P-G)  */
            MulSmall(s[0], s[0], 1); /* reduce s[0] */
            MulSmall(s[1], s[1], 1); /* reduce s[1] */


            /* prepare the chain  */
            for (i = 0; i < 32; i++)
            {
                vi = (vi >> 8) ^ (v[i] & 0xFF) ^ ((v[i] & 0xFF) << 1);
                hi = (hi >> 8) ^ (h[i] & 0xFF) ^ ((h[i] & 0xFF) << 1);
                nvh = ~(vi ^ hi);
                di = (nvh & (di & 0x80) >> 7) ^ vi;
                di ^= nvh & (di & 0x01) << 1;
                di ^= nvh & (di & 0x02) << 1;
                di ^= nvh & (di & 0x04) << 1;
                di ^= nvh & (di & 0x08) << 1;
                di ^= nvh & (di & 0x10) << 1;
                di ^= nvh & (di & 0x20) << 1;
                di ^= nvh & (di & 0x40) << 1;
                d[i] = (byte)di;
            }

            di = ((nvh & (di & 0x80) << 1) ^ vi) >> 8;

            /* initialize state */
            Set(yx[0], 1);
            Cpy(yx[1], q[di]);
            Cpy(yx[2], s[0]);
            Set(yz[0], 0);
            Set(yz[1], 1);
            Set(yz[2], 1);

            /* y[0] is (even)P + (even)G
             * y[1] is (even)P + (odd)G  if current d-bit is 0
             * y[1] is (odd)P + (even)G  if current d-bit is 1
             * y[2] is (odd)P + (odd)G
             */

            vi = 0;
            hi = 0;

            /* and go for it! */
            for (i = 32; i-- != 0; )
            {
                vi = (vi << 8) | (v[i] & 0xFF);
                hi = (hi << 8) | (h[i] & 0xFF);
                di = (di << 8) | (d[i] & 0xFF);

                for (j = 8; j-- != 0; )
                {
                    mont_prep(t1[0], t2[0], yx[0], yz[0]);
                    mont_prep(t1[1], t2[1], yx[1], yz[1]);
                    mont_prep(t1[2], t2[2], yx[2], yz[2]);

                    k = ((vi ^ vi >> 1) >> j & 1)
                        + ((hi ^ hi >> 1) >> j & 1);
                    mont_dbl(yx[2], yz[2], t1[k], t2[k], yx[0], yz[0]);

                    k = (di >> j & 2) ^ ((di >> j & 1) << 1);
                    mont_add(t1[1], t2[1], t1[k], t2[k], yx[1], yz[1],
                        q[di >> j & 1]);

                    mont_add(t1[2], t2[2], t1[0], t2[0], yx[2], yz[2],
                        s[((vi ^ hi) >> j & 2) >> 1]);
                }
            }

            k = (vi & 1) + (hi & 1);
            Recip(t1[0], yz[k], 0);
            Mul(t1[1], yx[k], t1[0]);

            Pack(t1[1], y);
        }

        public static bool IsCanonicalSignature(byte[] v)
        {
            var vCopy = v.Take(32).ToArray();
            var tmp = new byte[32];
            Divmod(tmp, vCopy, 32, Order, 32);
            for (var i = 0; i < 32; i++)
            {
                if (v[i] != vCopy[i])
                    return false;
            }
            return true;
        }

        public static bool IsCanonicalPublicKey(byte[] publicKey)
        {
            if (publicKey.Length != 32)
            {
                return false;
            }
            var publicKeyUnpacked = new Long10();
            Unpack(publicKeyUnpacked, publicKey);
            var publicKeyCopy = new byte[32];
            Pack(publicKeyUnpacked, publicKeyCopy);
            for (var i = 0; i < 32; i++)
            {
                if (publicKeyCopy[i] != publicKey[i])
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// sahn0:
        /// Using this class instead of long[10] to avoid bounds checks.
        /// </summary>
        private sealed class Long10
        {
            public Long10()
            {
            }

            // ReSharper disable InconsistentNaming
            public Long10(
                long _0, long _1, long _2, long _3, long _4,
                long _5, long _6, long _7, long _8, long _9)
            {
                this._0 = _0;
                this._1 = _1;
                this._2 = _2;
                this._3 = _3;
                this._4 = _4;
                this._5 = _5;
                this._6 = _6;
                this._7 = _7;
                this._8 = _8;
                this._9 = _9;
            }

            public long _0;
            public long _1;
            public long _2;
            public long _3;
            public long _4;
            public long _5;
            public long _6;
            public long _7;
            public long _8;
            public long _9;
            // ReSharper restore InconsistentNaming
        }

        /********************* radix 2^8 math *********************/

        private static void Cpy32(IList<byte> d, IList<byte> s)
        {
            int i;
            for (i = 0; i < 32; i++)
                d[i] = s[i];
        }

        /*  */
        /// <summary>
        /// p[m..n+m-1] = q[m..n+m-1] + z * x
        /// n is the size of x
        /// n+m is the size of p and q
        /// </summary>
        private static int mula_small(IList<byte> p, IList<byte> q, int m, IList<byte> x, int n, int z)
        {
            var v = 0;
            for (var i = 0; i < n; ++i)
            {
                v += (q[i + m] & 0xFF) + z * (x[i] & 0xFF);
                p[i + m] = (byte)v;
                v >>= 8;
            }
            return v;
        }

        /// <summary>
        /// p += x * y * z  where z is a small integer
        /// x is size 32, y is size t, p is size 32+t
        /// y is allowed to overlap with p+32 if you don't care about the upper half
        /// </summary>
        private static void Mula32(IList<byte> p, IList<byte> x, IList<byte> y, int t, int z)
        {
            const int n = 31;
            var w = 0;
            var i = 0;
            for (; i < t; i++)
            {
                var zy = z * (y[i] & 0xFF);
                w += mula_small(p, p, i, x, n, zy) +
                     (p[i + n] & 0xFF) + zy * (x[n] & 0xFF);
                p[i + n] = (byte)w;
                w >>= 8;
            }
            p[i + n] = (byte)(w + (p[i + n] & 0xFF));
        }

        /// <summary>
        /// divide r (size n) by d (size t), returning quotient q and remainder r
        /// quotient is size n-t+1, remainder is size t
        /// requires t > 0 && d[t-1] != 0
        /// requires that r[-1] and d[-1] are valid memory locations
        /// q may overlap with r+t
        /// </summary>
        private static void Divmod(IList<byte> q, IList<byte> r, int n, IList<byte> d, int t)
        {
            var rn = 0;
            var dt = (d[t - 1] & 0xFF) << 8;
            if (t > 1)
            {
                dt |= d[t - 2] & 0xFF;
            }
            while (n-- >= t)
            {
                var z = (rn << 16) | ((r[n] & 0xFF) << 8);
                if (n > 0)
                {
                    z |= r[n - 1] & 0xFF;
                }
                z /= dt;
                rn += mula_small(r, r, n - t + 1, d, t, -z);
                q[n - t + 1] = (byte)((z + rn) & 0xFF); /* rn is 0 or -1 (underflow) */
                mula_small(r, r, n - t + 1, d, t, -rn);
                rn = r[n] & 0xFF;
                r[n] = 0;
            }
            r[t - 1] = (byte)rn;
        }

        private static int Numsize(IList<byte> x, int n)
        {
            while (n-- != 0 && x[n] == 0)
            {
            }
            return n + 1;
        }

        /// <summary>
        /// Returns x if a contains the gcd, y if b.
        /// Also, the returned buffer contains the inverse of a mod b,
        /// as 32-byte signed.
        /// x and y must have 64 bytes space for temporary use.
        /// requires that a[-1] and b[-1] are valid memory locations
        /// </summary>
        private static byte[] Egcd32(byte[] x, byte[] y, IList<byte> a, IList<byte> b)
        {
            var bn = 32;
            int i;
            for (i = 0; i < 32; i++)
                x[i] = y[i] = 0;
            x[0] = 1;
            var an = Numsize(a, 32);
            if (an == 0)
                return y; /* division by zero */
            var temp = new byte[32];
            while (true)
            {
                var qn = bn - an + 1;
                Divmod(temp, b, bn, a, an);
                bn = Numsize(b, bn);
                if (bn == 0)
                    return x;
                Mula32(y, x, temp, qn, -1);

                qn = an - bn + 1;
                Divmod(temp, a, an, b, bn);
                an = Numsize(a, an);
                if (an == 0)
                    return y;
                Mula32(x, y, temp, qn, -1);
            }
        }

        /********************* radix 2^25.5 GF(2^255-19) math *********************/

        private const int P25 = 33554431;	/* (1 << 25) - 1 */
        private const int P26 = 67108863;	/* (1 << 26) - 1 */

        /// <summary>
        /// Convert to internal format from little-endian byte format
        /// </summary>
        private static void Unpack(Long10 x, IList<byte> m)
        {
            x._0 = m[0] & 0xFF | (m[1] & 0xFF) << 8 |
                   (m[2] & 0xFF) << 16 | (m[3] & 0xFF & 3) << 24;
            x._1 = (m[3] & 0xFF & ~3) >> 2 | (m[4] & 0xFF) << 6 |
                   (m[5] & 0xFF) << 14 | (m[6] & 0xFF & 7) << 22;
            x._2 = (m[6] & 0xFF & ~7) >> 3 | (m[7] & 0xFF) << 5 |
                   (m[8] & 0xFF) << 13 | (m[9] & 0xFF & 31) << 21;
            x._3 = (m[9] & 0xFF & ~31) >> 5 | (m[10] & 0xFF) << 3 |
                   (m[11] & 0xFF) << 11 | (m[12] & 0xFF & 63) << 19;
            x._4 = (m[12] & 0xFF & ~63) >> 6 | (m[13] & 0xFF) << 2 |
                   (m[14] & 0xFF) << 10 | (m[15] & 0xFF) << 18;
            x._5 = (m[16] & 0xFF) | (m[17] & 0xFF) << 8 |
                   (m[18] & 0xFF) << 16 | (m[19] & 0xFF & 1) << 24;
            x._6 = (m[19] & 0xFF & ~1) >> 1 | (m[20] & 0xFF) << 7 |
                   (m[21] & 0xFF) << 15 | (m[22] & 0xFF & 7) << 23;
            x._7 = (m[22] & 0xFF & ~7) >> 3 | (m[23] & 0xFF) << 5 |
                   (m[24] & 0xFF) << 13 | (m[25] & 0xFF & 15) << 21;
            x._8 = (m[25] & 0xFF & ~15) >> 4 | (m[26] & 0xFF) << 4 |
                   (m[27] & 0xFF) << 12 | (m[28] & 0xFF & 63) << 20;
            x._9 = (m[28] & 0xFF & ~63) >> 6 | (m[29] & 0xFF) << 2 |
                   (m[30] & 0xFF) << 10 | (m[31] & 0xFF) << 18;
        }

        /// <summary>
        /// Check if reduced-form input >= 2^255-19
        /// </summary>
        private static bool is_overflow(Long10 x)
        {
            return (
                x._0 > P26 - 19 &&
                ((x._1 & x._3 & x._5 & x._7 & x._9) == P25) &&
                ((x._2 & x._4 & x._6 & x._8) == P26)
                ) || (x._9 > P25);
        }

        /*   */

        /// <summary>
        /// Convert from internal format to little-endian byte format.  The
        /// number must be in a reduced form which is output by the following ops:
        /// unpack, mul, sqr
        /// set --  if input in range 0 .. P25
        /// If you're unsure if the number is reduced, first multiply it by 1.
        /// </summary>
        private static void Pack(Long10 x, IList<byte> m)
        {
            var ld = (is_overflow(x) ? 1 : 0) - (x._9 < 0 ? 1 : 0);
            var ud = ld * -(P25 + 1);
            ld *= 19;
            var t = ld + x._0 + (x._1 << 26);
            m[0] = (byte)t;
            m[1] = (byte)(t >> 8);
            m[2] = (byte)(t >> 16);
            m[3] = (byte)(t >> 24);
            t = (t >> 32) + (x._2 << 19);
            m[4] = (byte)t;
            m[5] = (byte)(t >> 8);
            m[6] = (byte)(t >> 16);
            m[7] = (byte)(t >> 24);
            t = (t >> 32) + (x._3 << 13);
            m[8] = (byte)t;
            m[9] = (byte)(t >> 8);
            m[10] = (byte)(t >> 16);
            m[11] = (byte)(t >> 24);
            t = (t >> 32) + (x._4 << 6);
            m[12] = (byte)t;
            m[13] = (byte)(t >> 8);
            m[14] = (byte)(t >> 16);
            m[15] = (byte)(t >> 24);
            t = (t >> 32) + x._5 + (x._6 << 25);
            m[16] = (byte)t;
            m[17] = (byte)(t >> 8);
            m[18] = (byte)(t >> 16);
            m[19] = (byte)(t >> 24);
            t = (t >> 32) + (x._7 << 19);
            m[20] = (byte)t;
            m[21] = (byte)(t >> 8);
            m[22] = (byte)(t >> 16);
            m[23] = (byte)(t >> 24);
            t = (t >> 32) + (x._8 << 12);
            m[24] = (byte)t;
            m[25] = (byte)(t >> 8);
            m[26] = (byte)(t >> 16);
            m[27] = (byte)(t >> 24);
            t = (t >> 32) + ((x._9 + ud) << 6);
            m[28] = (byte)t;
            m[29] = (byte)(t >> 8);
            m[30] = (byte)(t >> 16);
            m[31] = (byte)(t >> 24);
        }

        /// <summary>
        /// Copy a number
        /// </summary>
        private static void Cpy(Long10 valueOut, Long10 valueIn)
        {
            valueOut._0 = valueIn._0;
            valueOut._1 = valueIn._1;
            valueOut._2 = valueIn._2;
            valueOut._3 = valueIn._3;
            valueOut._4 = valueIn._4;
            valueOut._5 = valueIn._5;
            valueOut._6 = valueIn._6;
            valueOut._7 = valueIn._7;
            valueOut._8 = valueIn._8;
            valueOut._9 = valueIn._9;
        }

        /// <summary>
        /// Set a number to value, which must be in range -185861411 .. 185861411
        /// </summary>
        private static void Set(Long10 valueOut, int valueIn)
        {
            valueOut._0 = valueIn;
            valueOut._1 = 0;
            valueOut._2 = 0;
            valueOut._3 = 0;
            valueOut._4 = 0;
            valueOut._5 = 0;
            valueOut._6 = 0;
            valueOut._7 = 0;
            valueOut._8 = 0;
            valueOut._9 = 0;
        }

        /// <summary>
        /// Add/subtract two numbers.  The inputs must be in reduced form, and the
        /// output isn't, so to do another addition or subtraction on the output,
        /// first multiply it by one to reduce it.
        /// </summary>
        private static void Add(Long10 xy, Long10 x, Long10 y)
        {
            xy._0 = x._0 + y._0;
            xy._1 = x._1 + y._1;
            xy._2 = x._2 + y._2;
            xy._3 = x._3 + y._3;
            xy._4 = x._4 + y._4;
            xy._5 = x._5 + y._5;
            xy._6 = x._6 + y._6;
            xy._7 = x._7 + y._7;
            xy._8 = x._8 + y._8;
            xy._9 = x._9 + y._9;
        }

        private static void Sub(Long10 xy, Long10 x, Long10 y)
        {
            xy._0 = x._0 - y._0;
            xy._1 = x._1 - y._1;
            xy._2 = x._2 - y._2;
            xy._3 = x._3 - y._3;
            xy._4 = x._4 - y._4;
            xy._5 = x._5 - y._5;
            xy._6 = x._6 - y._6;
            xy._7 = x._7 - y._7;
            xy._8 = x._8 - y._8;
            xy._9 = x._9 - y._9;
        }

        /// <summary>
        /// Multiply a number by a small integer in range -185861411 .. 185861411.
        /// The output is in reduced form, the input x need not be.  x and xy may point
        /// to the same buffer.
        /// </summary>
        private static void MulSmall(Long10 xy, Long10 x, long y)
        {
            var t = x._8 * y;
            xy._8 = t & ((1 << 26) - 1);
            t = (t >> 26) + x._9 * y;
            xy._9 = t & ((1 << 25) - 1);
            t = 19 * (t >> 25) + x._0 * y;
            xy._0 = t & ((1 << 26) - 1);
            t = (t >> 26) + x._1 * y;
            xy._1 = t & ((1 << 25) - 1);
            t = (t >> 25) + x._2 * y;
            xy._2 = t & ((1 << 26) - 1);
            t = (t >> 26) + x._3 * y;
            xy._3 = t & ((1 << 25) - 1);
            t = (t >> 25) + x._4 * y;
            xy._4 = t & ((1 << 26) - 1);
            t = (t >> 26) + x._5 * y;
            xy._5 = t & ((1 << 25) - 1);
            t = (t >> 25) + x._6 * y;
            xy._6 = t & ((1 << 26) - 1);
            t = (t >> 26) + x._7 * y;
            xy._7 = t & ((1 << 25) - 1);
            t = (t >> 25) + xy._8;
            xy._8 = t & ((1 << 26) - 1);
            xy._9 += t >> 26;
        }

        /// <summary>
        /// Multiply two numbers.  The output is in reduced form, the inputs need not be
        /// </summary>
        private static void Mul(Long10 xy, Long10 x, Long10 y)
        {
            /* sahn0:
            * Using local variables to avoid class access.
            * This seem to improve performance a bit...
            */
            long
                x0 = x._0,
                x1 = x._1,
                x2 = x._2,
                x3 = x._3,
                x4 = x._4,
                x5 = x._5,
                x6 = x._6,
                x7 = x._7,
                x8 = x._8,
                x9 = x._9;
            long
                y0 = y._0,
                y1 = y._1,
                y2 = y._2,
                y3 = y._3,
                y4 = y._4,
                y5 = y._5,
                y6 = y._6,
                y7 = y._7,
                y8 = y._8,
                y9 = y._9;

            var t = x0 * y8 + x2 * y6 + x4 * y4 + x6 * y2 +
                     x8 * y0 + 2 * (x1 * y7 + x3 * y5 +
                                  x5 * y3 + x7 * y1) + 38*x9*y9;
            xy._8 = t & ((1 << 26) - 1);
            t = (t >> 26) + x0 * y9 + x1 * y8 + x2 * y7 +
                x3 * y6 + x4 * y5 + x5 * y4 +
                x6 * y3 + x7 * y2 + x8 * y1 +
                x9 * y0;
            xy._9 = t & ((1 << 25) - 1);
            t = x0 * y0 + 19 * ((t >> 25) + x2 * y8 + x4 * y6
                                + x6 * y4 + x8 * y2) + 38 *
                (x1 * y9 + x3 * y7 + x5 * y5 +
                 x7 * y3 + x9 * y1);
            xy._0 = t & ((1 << 26) - 1);
            t = (t >> 26) + x0 * y1 + x1 * y0 + 19 * (x2 * y9
                                                        + x3 * y8 + x4 * y7 + x5 * y6 +
                                                        x6 * y5 + x7 * y4 + x8 * y3 +
                                                        x9 * y2);
            xy._1 = t & ((1 << 25) - 1);
            t = (t >> 25) + x0 * y2 + x2 * y0 + 19 * (x4 * y8
                                                        + x6 * y6 + x8 * y4) + 2*x1*y1
                + 38 * (x3 * y9 + x5 * y7 +
                      x7 * y5 + x9 * y3);
            xy._2 = t & ((1 << 26) - 1);
            t = (t >> 26) + x0 * y3 + x1 * y2 + x2 * y1 +
                x3 * y0 + 19 * (x4 * y9 + x5 * y8 +
                                x6 * y7 + x7 * y6 +
                                x8 * y5 + x9 * y4);
            xy._3 = t & ((1 << 25) - 1);
            t = (t >> 25) + x0 * y4 + x2 * y2 + x4 * y0 + 19 *
                (x6 * y8 + x8 * y6) + 2 * (x1 * y3 +
                                             x3 * y1) + 38 *
                (x5 * y9 + x7 * y7 + x9 * y5);
            xy._4 = t & ((1 << 26) - 1);
            t = (t >> 26) + x0 * y5 + x1 * y4 + x2 * y3 +
                x3 * y2 + x4 * y1 + x5 * y0 + 19 *
                (x6 * y9 + x7 * y8 + x8 * y7 +
                 x9 * y6);
            xy._5 = t & ((1 << 25) - 1);
            t = (t >> 25) + x0 * y6 + x2 * y4 + x4 * y2 +
                x6 * y0 + 19*x8*y8 + 2 * (x1 * y5 +
                                              x3 * y3 + x5 * y1) + 38 *
                (x7 * y9 + x9 * y7);
            xy._6 = t & ((1 << 26) - 1);
            t = (t >> 26) + x0 * y7 + x1 * y6 + x2 * y5 +
                x3 * y4 + x4 * y3 + x5 * y2 +
                x6 * y1 + x7 * y0 + 19 * (x8 * y9 +
                                            x9 * y8);
            xy._7 = t & ((1 << 25) - 1);
            t = (t >> 25) + xy._8;
            xy._8 = t & ((1 << 26) - 1);
            xy._9 += t >> 26;
        }

        /// <summary>
        /// Square a number.  Optimization of  mul25519(x2, x, x)
        /// </summary>
        private static void Sqr(Long10 x10, Long10 x)
        {
            long
                x0 = x._0,
                x1 = x._1,
                x2 = x._2,
                x3 = x._3,
                x4 = x._4,
                x5 = x._5,
                x6 = x._6,
                x7 = x._7,
                x8 = x._8,
                x9 = x._9;

            var t = x4 * x4 + 2 * (x0 * x8 + x2 * x6) + 38*x9*x9 + 4 * (x1 * x7 + x3 * x5);
            x10._8 = t & ((1 << 26) - 1);
            t = (t >> 26) + 2 * (x0 * x9 + x1 * x8 + x2 * x7 +
                               x3 * x6 + x4 * x5);
            x10._9 = t & ((1 << 25) - 1);
            t = 19 * (t >> 25) + x0 * x0 + 38 * (x2 * x8 +
                                               x4 * x6 + x5 * x5) + 76 * (x1 * x9
                                                                            + x3 * x7);
            x10._0 = t & ((1 << 26) - 1);
            t = (t >> 26) + 2*x0*x1 + 38 * (x2 * x9 +
                                              x3 * x8 + x4 * x7 + x5 * x6);
            x10._1 = t & ((1 << 25) - 1);
            t = (t >> 25) + 19*x6*x6 + 2 * (x0 * x2 +
                                              x1 * x1) + 38*x4*x8 + 76 *
                (x3 * x9 + x5 * x7);
            x10._2 = t & ((1 << 26) - 1);
            t = (t >> 26) + 2 * (x0 * x3 + x1 * x2) + 38 *
                (x4 * x9 + x5 * x8 + x6 * x7);
            x10._3 = t & ((1 << 25) - 1);
            t = (t >> 25) + x2 * x2 + 2*x0*x4 + 38 *
                (x6 * x8 + x7 * x7) + 4*x1*x3 + 76*x5*x9;
            x10._4 = t & ((1 << 26) - 1);
            t = (t >> 26) + 2 * (x0 * x5 + x1 * x4 + x2 * x3)
                + 38 * (x6 * x9 + x7 * x8);
            x10._5 = t & ((1 << 25) - 1);
            t = (t >> 25) + 19*x8*x8 + 2 * (x0 * x6 +
                                              x2 * x4 + x3 * x3) + 4*x1*x5 +
                76*x7*x9;
            x10._6 = t & ((1 << 26) - 1);
            t = (t >> 26) + 2 * (x0 * x7 + x1 * x6 + x2 * x5 +
                               x3 * x4) + 38*x8*x9;
            x10._7 = t & ((1 << 25) - 1);
            t = (t >> 25) + x10._8;
            x10._8 = t & ((1 << 26) - 1);
            x10._9 += t >> 26;
        }

        /// <summary>
        /// Calculates a reciprocal.  The output is in reduced form, the inputs need not
        /// be.  Simply calculates  y = x^(p-2)  so it's not too fast.
        /// When sqrtassist is true, it instead calculates y = x^((p-5)/8)
        /// </summary>
        private static void Recip(Long10 y, Long10 x, int sqrtassist)
        {
            Long10
                t0 = new Long10(),
                t1 = new Long10(),
                t2 = new Long10(),
                t3 = new Long10(),
                t4 = new Long10();
            int i;
            /* the chain for x^(2^255-21) is straight from djb's implementation */
            Sqr(t1, x); /*  2 == 2 * 1	*/
            Sqr(t2, t1); /*  4 == 2 * 2	*/
            Sqr(t0, t2); /*  8 == 2 * 4	*/
            Mul(t2, t0, x); /*  9 == 8 + 1	*/
            Mul(t0, t2, t1); /* 11 == 9 + 2	*/
            Sqr(t1, t0); /* 22 == 2 * 11	*/
            Mul(t3, t1, t2); /* 31 == 22 + 9
                    == 2^5   - 2^0	*/
            Sqr(t1, t3); /* 2^6   - 2^1	*/
            Sqr(t2, t1); /* 2^7   - 2^2	*/
            Sqr(t1, t2); /* 2^8   - 2^3	*/
            Sqr(t2, t1); /* 2^9   - 2^4	*/
            Sqr(t1, t2); /* 2^10  - 2^5	*/
            Mul(t2, t1, t3); /* 2^10  - 2^0	*/
            Sqr(t1, t2); /* 2^11  - 2^1	*/
            Sqr(t3, t1); /* 2^12  - 2^2	*/
            for (i = 1; i < 5; i++)
            {
                Sqr(t1, t3);
                Sqr(t3, t1);
            } /* t3 */ /* 2^20  - 2^10	*/
            Mul(t1, t3, t2); /* 2^20  - 2^0	*/
            Sqr(t3, t1); /* 2^21  - 2^1	*/
            Sqr(t4, t3); /* 2^22  - 2^2	*/
            for (i = 1; i < 10; i++)
            {
                Sqr(t3, t4);
                Sqr(t4, t3);
            } /* t4 */ /* 2^40  - 2^20	*/
            Mul(t3, t4, t1); /* 2^40  - 2^0	*/
            for (i = 0; i < 5; i++)
            {
                Sqr(t1, t3);
                Sqr(t3, t1);
            } /* t3 */ /* 2^50  - 2^10	*/
            Mul(t1, t3, t2); /* 2^50  - 2^0	*/
            Sqr(t2, t1); /* 2^51  - 2^1	*/
            Sqr(t3, t2); /* 2^52  - 2^2	*/
            for (i = 1; i < 25; i++)
            {
                Sqr(t2, t3);
                Sqr(t3, t2);
            } /* t3 */ /* 2^100 - 2^50 */
            Mul(t2, t3, t1); /* 2^100 - 2^0	*/
            Sqr(t3, t2); /* 2^101 - 2^1	*/
            Sqr(t4, t3); /* 2^102 - 2^2	*/
            for (i = 1; i < 50; i++)
            {
                Sqr(t3, t4);
                Sqr(t4, t3);
            } /* t4 */ /* 2^200 - 2^100 */
            Mul(t3, t4, t2); /* 2^200 - 2^0	*/
            for (i = 0; i < 25; i++)
            {
                Sqr(t4, t3);
                Sqr(t3, t4);
            } /* t3 */ /* 2^250 - 2^50	*/
            Mul(t2, t3, t1); /* 2^250 - 2^0	*/
            Sqr(t1, t2); /* 2^251 - 2^1	*/
            Sqr(t2, t1); /* 2^252 - 2^2	*/
            if (sqrtassist != 0)
            {
                Mul(y, x, t2); /* 2^252 - 3 */
            }
            else
            {
                Sqr(t1, t2); /* 2^253 - 2^3	*/
                Sqr(t2, t1); /* 2^254 - 2^4	*/
                Sqr(t1, t2); /* 2^255 - 2^5	*/
                Mul(y, t1, t0); /* 2^255 - 21	*/
            }
        }

        /// <summary>
        /// checks if x is "negative", requires reduced input
        /// </summary>
        private static int IsNegative(Long10 x)
        {
            return (int)((is_overflow(x) || (x._9 < 0) ? 1 : 0) ^ (x._0 & 1));
        }

        /// <summary>
        /// a square root
        /// </summary>
        private static void Sqrt(Long10 x, Long10 u)
        {
            Long10 v = new Long10(), t1 = new Long10(), t2 = new Long10();
            Add(t1, u, u); /* t1 = 2u		*/
            Recip(v, t1, 1); /* v = (2u)^((p-5)/8)	*/
            Sqr(x, v); /* x = v^2		*/
            Mul(t2, t1, x); /* t2 = 2uv^2		*/
            t2._0--; /* t2 = 2uv^2-1		*/
            Mul(t1, v, t2); /* t1 = v(2uv^2-1)	*/
            Mul(x, u, t1); /* x = uv(2uv^2-1)	*/
        }

        /********************* Elliptic curve *********************/

        /// <summary>
        /// y^2 = x^3 + 486662 x^2 + x  over GF(2^255-19)
        /// t1 = ax + az
        /// t2 = ax - az
        /// </summary>
        private static void mont_prep(Long10 t1, Long10 t2, Long10 ax, Long10 az)
        {
            Add(t1, ax, az);
            Sub(t2, ax, az);
        }

        /// <summary>
        /// A = P + Q   where
        /// X(A) = ax/az
        /// X(P) = (t1+t2)/(t1-t2)
        /// X(Q) = (t3+t4)/(t3-t4)
        /// X(P-Q) = dx
        /// clobbers t1 and t2, preserves t3 and t4
        /// </summary>
        private static void mont_add(Long10 t1, Long10 t2, Long10 t3, Long10 t4, Long10 ax, Long10 az, Long10 dx)
        {
            Mul(ax, t2, t3);
            Mul(az, t1, t4);
            Add(t1, ax, az);
            Sub(t2, ax, az);
            Sqr(ax, t1);
            Sqr(t1, t2);
            Mul(az, t1, dx);
        }

        /// <summary>
        /// B = 2 * Q   where
        /// X(B) = bx/bz
        /// X(Q) = (t3+t4)/(t3-t4)
        /// clobbers t1 and t2, preserves t3 and t4
        /// </summary>
        private static void mont_dbl(Long10 t1, Long10 t2, Long10 t3, Long10 t4, Long10 bx, Long10 bz)
        {
            Sqr(t1, t3);
            Sqr(t2, t4);
            Mul(bx, t1, t2);
            Sub(t2, t1, t2);
            MulSmall(bz, t2, 121665);
            Add(t1, t1, bz);
            Mul(bz, t1, t2);
        }

        /// <summary>
        /// Y^2 = X^3 + 486662 X^2 + X
        /// t is a temporary
        /// </summary>
        private static void x_to_y2(Long10 t, Long10 y2, Long10 x)
        {
            Sqr(t, x);
            MulSmall(y2, x, 486662);
            Add(t, t, y2);
            t._0++;
            Mul(y2, t, x);
        }

        /// <summary>
        /// P = kG   and  s = sign(P)/k
        /// </summary>
        private static void Core(IList<byte> px, IList<byte> s, IList<byte> k, IList<byte> gx)
        {
            Long10
                dx = new Long10(),
                t1 = new Long10(),
                t2 = new Long10(),
                t3 = new Long10(),
                t4 = new Long10();
            Long10[]
                x = { new Long10(), new Long10() },
                z = { new Long10(), new Long10() };
            int i;

            /* unpack the base */
            if (gx != null)
                Unpack(dx, gx);
            else
                Set(dx, 9);

            /* 0G = point-at-infinity */
            Set(x[0], 1);
            Set(z[0], 0);

            /* 1G = G */
            Cpy(x[1], dx);
            Set(z[1], 1);

            for (i = 32; i-- != 0; )
            {
                if (i == 0)
                {
                    i = 0;
                }
                int j;
                for (j = 8; j-- != 0; )
                {
                    /* swap arguments depending on bit */
                    var bit1 = (k[i] & 0xFF) >> j & 1;
                    var bit0 = ~(k[i] & 0xFF) >> j & 1;
                    var ax = x[bit0];
                    var az = z[bit0];
                    var bx = x[bit1];
                    var bz = z[bit1];

                    /* a' = a + b	*/
                    /* b' = 2 b	*/
                    mont_prep(t1, t2, ax, az);
                    mont_prep(t3, t4, bx, bz);
                    mont_add(t1, t2, t3, t4, ax, az, dx);
                    mont_dbl(t1, t2, t3, t4, bx, bz);
                }
            }

            Recip(t1, z[0], 0);
            Mul(dx, x[0], t1);
            Pack(dx, px);

            /* calculate s such that s abs(P) = G  .. assumes G is std base point */
            if (s != null)
            {
                x_to_y2(t2, t1, dx); /* t1 = Py^2  */
                Recip(t3, z[1], 0); /* where Q=P+G ... */
                Mul(t2, x[1], t3); /* t2 = Qx  */
                Add(t2, t2, dx); /* t2 = Qx + Px  */
                t2._0 += 9 + 486662; /* t2 = Qx + Px + Gx + 486662  */
                dx._0 -= 9; /* dx = Px - Gx  */
                Sqr(t3, dx); /* t3 = (Px - Gx)^2  */
                Mul(dx, t2, t3); /* dx = t2 (Px - Gx)^2  */
                Sub(dx, dx, t1); /* dx = t2 (Px - Gx)^2 - Py^2  */
                dx._0 -= 39420360; /* dx = t2 (Px - Gx)^2 - Py^2 - Gy^2  */
                Mul(t1, dx, BaseR2Y); /* t1 = -Py  */
                if (IsNegative(t1) != 0) /* sign is 1, so just copy  */
                    Cpy32(s, k);
                else /* sign is -1, so negate  */
                    mula_small(s, OrderTimes8, 0, k, 32, -1);

                /* reduce s mod q
             * (is this needed?  do it just in case, it's fast anyway) */
                //divmod((dstptr) t1, s, 32, order25519, 32);

                /* take reciprocal of s mod q */
                var temp1 = new byte[32];
                var temp2 = new byte[64];
                var temp3 = new byte[64];
                Cpy32(temp1, Order);
                Cpy32(s, Egcd32(temp2, temp3, s, temp1));
                if ((s[31] & 0x80) != 0)
                    mula_small(s, s, 0, Order, 32, 1);
            }
        }

        /* smallest multiple of the order that's >= 2^255 */
        private static readonly byte[] OrderTimes8 =
        {
            104, 159, 174, 231,
            210, 24, 147, 192,
            178, 230, 188, 23,
            245, 206, 247, 166,
            0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 0,
            0, 0, 0, 128
        };

        /* constants 2Gy and 1/(2Gy) */

        private static readonly Long10 Base2Y = new Long10(
            39999547, 18689728, 59995525, 1648697, 57546132,
            24010086, 19059592, 5425144, 63499247, 16420658
            );

        private static readonly Long10 BaseR2Y = new Long10(
            5744, 8160848, 4790893, 13779497, 35730846,
            12541209, 49101323, 30047407, 40071253, 6226132
            );
    }
}
