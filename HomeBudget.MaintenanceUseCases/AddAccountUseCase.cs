  using System;
  using HomeBudget.Common;
  using HomeBudget.MaintenanceUseCases.Request;
  using HomeBudget.MaintenanceUseCases.RequestDto;

namespace HomeBudget.MaintenanceUseCases
{
    public class AddAccountUseCase : IUseCase
    {
        private readonly AccountsRepository _repos;
        private const decimal DefaultBalance = 0M;
        private const string DefaultName = "Account1";

        public AddAccountUseCase(IRepository repos)
        {
            _repos = (AccountsRepository)repos;
        }

        public void Execute(IRequest request)
        {
            var rq = (MaintenanceRequest<AccountDto>) request;

            var account = GetDefaults(rq.Dto);
            
            _repos.AddAccount(account);

            MaintenanceEvents.BroadcastAccount(new AccountInfoDto(account));
        }

        private AccountDto GetDefaults(AccountDto dto)
        {
            if (dto == null)
            {
                return new AccountDto(DefaultName, DefaultBalance);
            }
            if (String.IsNullOrWhiteSpace(dto.Name))
            {
                dto.Name = DefaultName;
            }
            return dto;
        }
    }
}