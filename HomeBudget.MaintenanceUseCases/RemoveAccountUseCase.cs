using HomeBudget.Common;
using HomeBudget.MaintenanceUseCases.Request;
using HomeBudget.MaintenanceUseCases.RequestDto;

namespace HomeBudget.MaintenanceUseCases
{
    public class RemoveAccountUseCase : IUseCase
    {
        private readonly AccountsRepository _repos;

        public RemoveAccountUseCase(IRepository repos)
        {
            _repos = (AccountsRepository)repos;
        }

        public void Execute(IRequest req)
        {
            var request = (MaintenanceRequest<string>) req;
            _repos.RemoveAccount(request.Dto);

            MaintenanceEvents.BroadcastRemoveAccount(new AccountInfoDto(request.Dto));
        }
    }
}