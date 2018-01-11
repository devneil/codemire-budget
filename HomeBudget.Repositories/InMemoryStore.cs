using System;
using System.Collections.Generic;
using System.Linq;
using HomeBudget.Common;
using HomeBudget.MaintenanceUseCases;
using HomeBudget.MaintenanceUseCases.RequestDto;
using HomeBudget.OperationUseCases;
using HomeBudget.OperationUseCases.RequestDto;


namespace HomeBudget.Repositories
{
    public class InMemoryStore : AccountsRepository, IOperationRepository
    {
        protected readonly List<AccountDto> Accounts = new List<AccountDto>();
        protected readonly List<ITransactionDto> Transactions = new List<ITransactionDto>();

        protected override int GetNumberOfAccountsNamed(string name, string separator)
        {
            return Accounts.Count(acc => (acc.Name == name) || (acc.Name.StartsWith(name + separator)));
        }

        protected override void Save(AccountDto account)
        {
            Accounts.Add(account);
        }

        public override void RemoveAccount(string accountName)
        {
            Accounts.RemoveAll(acc => acc.Name == accountName);
        }

        public override void AddIncome(ITransactionDto transaction)
        {
            Transactions.Add(transaction);
        }

        public override void RemoveTransaction(ITransactionDto transaction)
        {
            var tran =
                Transactions.Find(
                    t =>
                        t.AccountName == transaction.AccountName && t.Date == transaction.Date &&
                        t.Value == transaction.Value);
            if (tran != null)Transactions.Remove(tran);
        }

        public decimal GetAccountBalance(string accountName)
        {
            return Accounts.Find(acc => acc.Name == accountName).Balance;
        }

        public ITransactionDto[] GetAccountTransactions()
        {
            return Transactions.ConvertAll(input => new TransactionInfoDto())
        }

        public AccountBalanceInfoDto[] GetAccountBalances()
        {
            return
                Accounts.ConvertAll(
                    input => new AccountBalanceInfoDto(input.Name, input.Balance)).ToArray();
        }
    }

}
