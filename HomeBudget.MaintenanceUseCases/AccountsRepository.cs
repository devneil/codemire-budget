using HomeBudget.Common;
using HomeBudget.MaintenanceUseCases.RequestDto;

namespace HomeBudget.MaintenanceUseCases
{
    public abstract class AccountsRepository : IRepository
    {
        public void AddAccount(AccountDto account)
        {
            RenameAccountIfNeeded(account);
            Save(account);
        }

        private void RenameAccountIfNeeded(AccountDto account)
        {
            const string separator = "_";
            int numberExisting = GetNumberOfAccountsNamed(account.Name, separator);

            if (numberExisting > 0)
            {
                account.Name = account.Name + separator + numberExisting;
            }
        }

        protected abstract int GetNumberOfAccountsNamed(string name, string separator);

        protected abstract void Save(AccountDto account);

        public abstract void RemoveAccount(string accountName);

        public abstract void AddIncome(ITransactionDto transaction);

        public abstract void RemoveTransaction(ITransactionDto transaction);
    }
}