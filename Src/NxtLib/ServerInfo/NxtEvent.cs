using System;

namespace NxtLib.ServerInfo
{
    [Flags]
    public enum NxtEvent
    {
        [NxtApi("Block.BLOCK_GENERATED")]
        BlockGenerated = 1,
        [NxtApi("Block.BLOCK_POPPED")]
        BlockPopped = 2,
        [NxtApi("Block.BLOCK_PUSHED")]
        BlockPushed = 4,
        [NxtApi("Peer.ADD_INBOUND")]
        PeerAddInbound = 8,
        [NxtApi("Peer.ADDED_ACTIVE_PEER")]
        PeerAddedActivePeer = 16,
        [NxtApi("Peer.BLACKLIST")]
        PeerBlacklist = 32,
        [NxtApi("Peer.CHANGED_ACTIVE_PEER")]
        PeerChangedActivePeer = 64,
        [NxtApi("Peer.DEACTIVATE")]
        PeerDecativete = 128,
        [NxtApi("Peer.NEW_PEER")]
        PeerNewPeer = 256,
        [NxtApi("Peer.REMOVE")]
        PeerRemove = 512,
        [NxtApi("Peer.REMOVE_INBOUND")]
        PeerRemoveInbound = 1024,
        [NxtApi("Peer.UNBLACKLIST")]
        PeerUnblacklist = 2048,
        [NxtApi("Transaction.ADDED_CONFIRMED_TRANSACTIONS")]
        TransactionAddedConfirmedTransactions = 4096,
        [NxtApi("Transaction.ADDED_UNCONFIRMED_TRANSACTIONS")]
        TransactionAddedUnconfirmedTransactions = 8192,
        [NxtApi("Transaction.REJECT_PHASED_TRANSACTION")]
        TransactionRejectPhasedTransaction = 16384,
        [NxtApi("Transaction.RELEASE_PHASED_TRANSACTION")]
        TransactionReleasePhasedTransaction = 32768,
        [NxtApi("Transaction.REMOVE_UNCONFIRMED_TRANSACTIONS")]
        TransactionRemoveUnconfirmedTransactions = 65536
    }
}