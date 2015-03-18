namespace NxtExchange.DAL
{
    public interface INxtContextFactory
    {
        INxtContext Create();
    }

    public class NxtContextFactory : INxtContextFactory
    {
        public INxtContext Create()
        {
            return new NxtContext();
        }
    }
}
