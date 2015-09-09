using System;
using HomeBudget.Common;
using HomeBudget.MaintenanceUseCases;
using HomeBudget.MaintenanceUseCases.RequestDto;

namespace HomeBudget.RequestTest
{
    public class TestAccountsRepository : AccountsRepository
    {
        private int _numAccounts;
        public bool AddAccountCalled { get; private set; }
        public string AddAccountName { get; private set; }
        public decimal AddAccountBalance { get; private set; }
        public bool RemoveAccountCalled { get; private set; }
        public string RemoveAccountName { get; private set; }
        public bool AddTranCalled { get; private set; }
        public decimal AddTranValue { get; private set; }
        public DateTime AddTranDate { get; private set; }
        public string AddTranAccount { get; private set; }
        public bool RemoveTransactionCalled { get; set; }

        public void SetNumAccounts(int number)
        {
            _numAccounts = number;
        }

        protected override void Save(AccountDto account)
        {
            AddAccountCalled = true;
            AddAccountName = account.Name;
            AddAccountBalance = account.Balance;
        }

        protected override int GetNumberOfAccountsNamed(string name, string separator)
        {
            return _numAccounts;
        }

        public override void RemoveAccount(string accountName)
        {
            RemoveAccountCalled = true;
            RemoveAccountName = accountName;
        }

        public override void AddIncome(ITransactionDto transaction)
        {
            AddTranCalled = true;
            AddTranValue = transaction.Value;
            AddTranDate = transaction.Date;
            AddTranAccount = transaction.AccountName;
        }

        public override void RemoveTransaction(ITransactionDto transaction)
        {
            RemoveTransactionCalled = true;
            RemoveAccountName = transaction.AccountName;
        }
    }
}

        