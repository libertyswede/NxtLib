using System.Collections.Generic;
using System.Linq;

namespace NxtLib
{
    public static class TransactionTypeMapper
    {
        private static readonly Dictionary<byte, TransactionMainType> MainTypes;
        private static readonly Dictionary<byte, Dictionary<byte, TransactionSubType>> SubTypes;

        private static readonly Dictionary<TransactionMainType, byte> MainTypesToByte = new Dictionary<TransactionMainType, byte>();
        private static readonly Dictionary<TransactionSubType, byte> SubTypesToByte = new Dictionary<TransactionSubType, byte>();

        static TransactionTypeMapper()
        {
            MainTypes = new Dictionary<byte, TransactionMainType>
            {
                {0, TransactionMainType.Payment},
                {1, TransactionMainType.Messaging},
                {2, TransactionMainType.ColoredCoins},
                {3, TransactionMainType.DigitalGoods},
                {4, TransactionMainType.AccountControl},
                {5, TransactionMainType.MonetarySystem},
                {6, TransactionMainType.TaggedData},
                {7, TransactionMainType.Shuffling}
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
                        {8, TransactionSubType.MessagingAliasDelete},
                        {9, TransactionSubType.MessagingPhasingVoteCasting},
                        {10, TransactionSubType.MessagingAccountProperty},
                        {11, TransactionSubType.MessagingAccountPropertyDelete}
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
                        {6, TransactionSubType.ColoredCoinsDividendPayment},
                        {7, TransactionSubType.ColoredCoinsAssetDelete}
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
                        {0, TransactionSubType.AccountControlEffectiveBalanceLeasing},
                        {1, TransactionSubType.AccountControlSetPhasingOnly}
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
                },
                {
                    6, new Dictionary<byte, TransactionSubType>
                    {
                        {0, TransactionSubType.TaggedDataUpload},
                        {1, TransactionSubType.TaggedDataExtend}
                    }
                },
                {
                    7, new Dictionary<byte, TransactionSubType>
                    {
                        {0, TransactionSubType.ShufflingCreation},
                        {1, TransactionSubType.ShufflingRegistration},
                        {2, TransactionSubType.ShufflingProcessing},
                        {3, TransactionSubType.ShufflingRecipients},
                        {4, TransactionSubType.ShufflingVerification}
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

        public static TransactionMainType GetMainType(byte typeByte)
        {
            return MainTypes[typeByte];
        }

        public static TransactionSubType GetSubType(byte typeByte, byte subTypeByte)
        {
            return SubTypes[typeByte][subTypeByte];
        }

        public static byte GetMainTypeByte(TransactionMainType type)
        {
            return MainTypesToByte[type];
        }

        public static byte GetMainTypeByte(TransactionSubType type)
        {
            return SubTypes.Single(st => st.Value.ContainsValue(type)).Key;
        }

        public static byte GetSubTypeByte(TransactionSubType type)
        {
            return SubTypesToByte[type];
        }
    }
}