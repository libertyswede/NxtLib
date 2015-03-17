namespace NxtExchange
{
    public class ExchangeController
    {
        private readonly INxtService _nxtService;
        private readonly IRepository _repository;

        public ExchangeController(INxtService nxtService, IRepository repository)
        {
            _nxtService = nxtService;
            _repository = repository;
        }
    }

    public interface IRepository
    {
    }
}
