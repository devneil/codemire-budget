using HomeBudget.Common;
using HomeBudget.MaintenanceUseCases.Request;
using HomeBudget.MaintenanceUseCases.RequestDto;

namespace HomeBudget.MaintenanceUseCases
{
    public class RemoveTransactionUseCase : IUseCase
    {
        private readonly AccountsRepository _repos;

        public RemoveTransactionUseCase(IRepository repos)
        {
            _repos = (AccountsRepository)repos;
        }

        public void Execute(IRequest request)
        {
            var req = (MaintenanceRequest<ITransactionDto>)request;
            _repos.RemoveTransaction(req.Dto);

            MaintenanceEvents.BroadcastRemoveTransaction(new TransactionInfoDto(req.Dto));


        }
    }
}