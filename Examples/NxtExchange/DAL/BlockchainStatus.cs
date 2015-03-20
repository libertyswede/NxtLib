using System;

namespace NxtExchange.DAL
{
    public class BlockchainStatus
    {
        public BlockchainStatus()
        {
            Id = 1;
        }

        public int Id { get; set; }

        /// <summary>
        /// The last block processed by the exchange
        /// </summary>
        public long LastKnownBlockId { get; set; }
        public DateTime LastKnownBlockTimestamp { get; set; }

        /// <summary>
        /// Confirmed means it has 10 confirmations
        /// </summary>
        public long LastConfirmedBlockId { get; set; }
        public DateTime LastConfirmedBlockTimestamp { get; set; }

        /// <summary>
        /// Secure means it has 720 confirmations, and thus cannot be a part of a block re-organization.
        /// </summary>
        public long LastSecureBlockId { get; set; }
        public DateTime LastSecureBlockTimestamp { get; set; }
    }
}
