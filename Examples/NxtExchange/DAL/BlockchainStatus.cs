namespace NxtExchange.DAL
{
    public class BlockchainStatus
    {
        public int Id { get; set; }

        /// <summary>
        /// Secure means it has 720 confirmations, and thus cannot be a part of a block re-organization.
        /// </summary>
        public long LastSecureBlockId { get; set; }

        /// <summary>
        /// The last block processed by the exchange
        /// </summary>
        public long LastKnownBlockId { get; set; }
    }
}
