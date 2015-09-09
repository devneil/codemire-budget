using HomeBudget.MaintenanceUseCases.RequestDto;

namespace HomeBudget.MaintenanceUseCases
{
    public class AccountInfoDto
    {
        public AccountInfoDto(AccountDto account)
        {
            Name = account.Name;
            Balance = account.Balance;
        }

        public AccountInfoDto(string accountName)
        {
            Name = accountName;
        }

        public string Name { get; private set; }
        public decimal Balance { get; private set; }
    }
}