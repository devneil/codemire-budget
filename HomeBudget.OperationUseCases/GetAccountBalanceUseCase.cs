using HomeBudget.Common;
using HomeBudget.OperationUseCases.Request;
using HomeBudget.OperationUseCases.RequestDto;

namespace HomeBudget.OperationUseCases
{
    public class GetAccountBalanceUseCase :IUseCase
    {
        private readonly IOperationRepository _repos;

        public GetAccountBalanceUseCase(IRepository repos)
        {
            _repos = (IOperationRepository)repos;
        }

        public void Execute(IRequest request)
        {
            var rq = (OperationRequest<string>)request;

            var accountName = rq.Dto;
            var balance = _repos.GetAccountBalance(accountName);

            OperationEvents.BroadcastAccountBalance(new AccountBalanceInfoDto(accountName, balance));
        }
    }
}