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
        public int LastKnownBlockHeight { get; set; }

        /// <summary>
        /// Confirmed means it has 10 confirmations
        /// </summary>
        public long LastConfirmedBlockId { get; set; }
        public int LastConfirmedBlockHeight { get; set; }

        /// <summary>
        /// Secure means it has 720 confirmations, and thus cannot be a part of a block re-organization.
        /// </summary>
        public long LastSecureBlockId { get; set; }
        public int LastSecureBlockHeight { get; set; }
    }
}
