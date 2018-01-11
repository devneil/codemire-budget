using HomeBudget.Common;
using HomeBudget.MaintenanceUseCases.Request;
using HomeBudget.MaintenanceUseCases.RequestDto;

namespace HomeBudget.MaintenanceUseCases
{
    public class AddTransactionUseCase : IUseCase
    {
        private readonly AccountsRepository _repos;

        public AddTransactionUseCase(IRepository repos)
        {
            _repos = (AccountsRepository)repos;
        }

        public void Execute(IRequest request)
        {
            var req = (MaintenanceRequest<ITransactionDto>)request;
            _repos.AddIncome(req.Dto);

            MaintenanceEvents.BroadcastTransaction(new TransactionInfoDto(req.Dto));
        }
    }
}