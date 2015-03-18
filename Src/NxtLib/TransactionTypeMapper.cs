using System.Collections.Generic;
using System.Linq;

namespace NxtLib
{
    internal static class TransactionTypeMapper
    {
        private readonly static Dictionary<byte, TransactionMainType> MainTypes;
        private readonly static Dictionary<byte, Dictionary<byte, TransactionSubType>> SubTypes;

        private readonly static Dictionary<TransactionMainType, byte> MainTypesToByte = new Dictionary<TransactionMainType, byte>();
        private readonly static Dictionary<TransactionSubType, byte> SubTypesToByte = new Dictionary<TransactionSubType, byte>();

        static TransactionTypeMapper()
        {
            MainTypes = new Dictionary<byte, TransactionMainType>
            {
                {0, TransactionMainType.Payment},
                {1, TransactionMainType.Messaging},
                {2, TransactionMainType.ColoredCoins},
                {3, TransactionMainType.DigitalGoods},
                {4, TransactionMainType.AccountControl},
                {5, TransactionMainType.MonetarySystem}
            };

            SubTypes = new Dictionary<byte, Dictionary<byte, TransactionSubType>>
            {
                {
                    0, new Dictionary<byte, TransactionSubType>
                    {
                        {0, TransactionSubType.PaymentOrdinaryPayment}
                    }
                },
                {
                    1, new Dictionary<byte, TransactionSubType>
                    {
                        {0, TransactionSubType.MessagingArbitraryMessage},
                        {1, TransactionSubType.MessagingAliasAssignment},
                        {2, TransactionSubType.MessagingPollCreation},
                        {3, TransactionSubType.MessagingVoteCasting},
                        {4, TransactionSubType.MessagingHubTerminalAnnouncement},
                        {5, TransactionSubType.MessagingAccountInfo},
                        {6, TransactionSubType.MessagingAliasSell},
                        {7, TransactionSubType.MessagingAliasBuy},
                        {8, TransactionSubType.MessagingAliasDelete}
                    }
                },
                {
                    2, new Dictionary<byte, TransactionSubType>
                    {
                        {0, TransactionSubType.ColoredCoinsAssetIssuance},
                        {1, TransactionSubType.ColoredCoinsAssetTransfer},
                        {2, TransactionSubType.ColoredCoinsAskOrderPlacement},
                        {3, TransactionSubType.ColoredCoinsBidOrderPlacement},
                        {4, TransactionSubType.ColoredCoinsAskOrderCancellation},
                        {5, TransactionSubType.ColoredCoinsBidOrderCancellation},
                        {6, TransactionSubType.ColoredCoinsDividendPayment}
                    }
                },
                {
                    3, new Dictionary<byte, TransactionSubType>
                    {
                        {0, TransactionSubType.DigitalGoodsListing},
                        {1, TransactionSubType.DigitalGoodsDelisting},
                        {2, TransactionSubType.DigitalGoodsPriceChange},
                        {3, TransactionSubType.DigitalGoodsQuantityChange},
                        {4, TransactionSubType.DigitalGoodsPurchase},
                        {5, TransactionSubType.DigitalGoodsDelivery},
                        {6, TransactionSubType.DigitalGoodsFeedback},
                        {7, TransactionSubType.DigitalGoodsRefund}
                    }
                },
                {
                    4, new Dictionary<byte, TransactionSubType>
                    {
                        {0, TransactionSubType.AccountControlEffectiveBalanceLeasing}
                    }
                },
                {
                    5, new Dictionary<byte, TransactionSubType>
                    {
                        {0, TransactionSubType.MonetarySystemCurrencyIssuance},
                        {1, TransactionSubType.MonetarySystemReserveIncrease},
                        {2, TransactionSubType.MonetarySystemReserveClaim},
                        {3, TransactionSubType.MonetarySystemCurrencyTransfer},
                        {4, TransactionSubType.MonetarySystemPublishExchangeOffer},
                        {5, TransactionSubType.MonetarySystemExchangeBuy},
                        {6, TransactionSubType.MonetarySystemExchangeSell},
                        {7, TransactionSubType.MonetarySystemCurrencyMinting},
                        {8, TransactionSubType.MonetarySystemCurrencyDeletion}
                    }
                }
            };

            foreach (var mainType in MainTypes)
            {
                MainTypesToByte.Add(mainType.Value, mainType.Key);
            }

            foreach (var type in SubTypes.SelectMany(subType => subType.Value))
            {
                SubTypesToByte.Add(type.Value, type.Key);
            }
        }

        internal static TransactionMainType GetMainType(byte typeByte)
        {
            return MainTypes[typeByte];
        }

        internal static TransactionSubType GetSubType(byte typeByte, byte subTypeByte)
        {
            return SubTypes[typeByte][subTypeByte];
        }

        internal static byte GetMainTypeByte(TransactionMainType type)
        {
            return MainTypesToByte[type];
        }

        internal static byte GetMainTypeByte(TransactionSubType type)
        {
            return SubTypes.Single(st => st.Value.ContainsValue(type)).Key;
        }

        internal static byte GetSubTypeByte(TransactionSubType type)
        {
            return SubTypesToByte[type];
        }
    }
}