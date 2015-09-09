namespace HomeBudget.MaintenanceUseCases.RequestDto
{
    public class AccountDto
    {
        public AccountDto(string accountName, decimal balance)
        {
            Name = accountName;
            Balance = balance;
        }

        public string Name { get; set; }
        public decimal Balance { get; set; }
    }
}