using NxtLib.Accounts;
using NxtLib.Aliases;
using NxtLib.AssetExchange;
using NxtLib.Blocks;
using NxtLib.Debug;
using NxtLib.DigitalGoodsStore;
using NxtLib.Forging;
using NxtLib.Messages;
using NxtLib.MonetarySystem;
using NxtLib.Networking;
using NxtLib.ServerInfo;
using NxtLib.Tokens;
using NxtLib.Transactions;
using NxtLib.Utils;

namespace NxtLib
{
    public interface IServiceFactory
    {
        IAccountService CreateAccountService();
        IAliasService CreateAliasService();
        IAssetExchangeService CreateAssetExchangeService();
        IBlockService CreateBlockService();
        IDebugService CreateDebugService();
        IDigitalGoodsStoreService CreateDigitalGoodsStoreService();
        IForgingService CreateForgingService();
        IMessageService CreateMessageService();
        IMonetarySystemService CreateMonetarySystemService();
        INetworkingService CreateNetworkingService();
        IServerInfoService CreateServerInfoService();
        ITokenService CreateTokenService();
        ITransactionService CreateTransactionService();
        IUtilService CreateUtilService();
    }

    public class ServiceFactory : IServiceFactory
    {
        private readonly string _baseAddress;

        public ServiceFactory(string baseAddress = BaseService.DefaultBaseUrl)
        {
            _baseAddress = baseAddress;
        }

        public IAccountService CreateAccountService()
        {
            return new AccountService(_baseAddress);
        }

        public IAliasService CreateAliasService()
        {
            return new AliasService(_baseAddress);
        }

        public IAssetExchangeService CreateAssetExchangeService()
        {
            return new AssetExchangeService(_baseAddress);
        }

        public IBlockService CreateBlockService()
        {
            return new BlockService(_baseAddress);
        }

        public IDebugService CreateDebugService()
        {
            return new DebugService(_baseAddress);
        }

        public IDigitalGoodsStoreService CreateDigitalGoodsStoreService()
        {
            return new DigitalGoodsStoreService(_baseAddress);
        }

        public IForgingService CreateForgingService()
        {
            return new ForgingService(_baseAddress);
        }

        public IMessageService CreateMessageService()
        {
            return new MessageService(_baseAddress);
        }

        public IMonetarySystemService CreateMonetarySystemService()
        {
            return new MonetarySystemService(_baseAddress);
        }

        public INetworkingService CreateNetworkingService()
        {
            return new NetworkingService(_baseAddress);
        }

        public IServerInfoService CreateServerInfoService()
        {
            return new ServerInfoService(_baseAddress);
        }

        public ITokenService CreateTokenService()
        {
            return new TokenService(_baseAddress);
        }

        public ITransactionService CreateTransactionService()
        {
            return new TransactionService(_baseAddress);
        }

        public IUtilService CreateUtilService()
        {
            return new UtilService(_baseAddress);
        }
    }
}
