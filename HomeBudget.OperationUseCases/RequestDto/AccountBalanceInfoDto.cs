namespace HomeBudget.OperationUseCases.RequestDto
{
    public class AccountBalanceInfoDto
    {
        public string AccountName { get; set; }
        public decimal Balance { get; set; }

        public AccountBalanceInfoDto(string accountName, decimal balance)
        {
            AccountName = accountName;
            Balance = balance;
        }
    }
}