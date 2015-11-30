using NxtLib.Local;

namespace NxtLib.AccountControl
{
    public class AccountControlService : BaseService, IAccountControlService
    {
        public AccountControlService(string baseAddress = Constants.DefaultNxtUrl)
            : base(baseAddress)
        {
            
        }
    }
}
