using System.Threading.Tasks;
using NxtLib.AccountOperations;

namespace NxtConsoleDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var nxtManager = new NxtManager(new AccountService());
            Task.WaitAll(nxtManager.Start());
        }
    }
}
