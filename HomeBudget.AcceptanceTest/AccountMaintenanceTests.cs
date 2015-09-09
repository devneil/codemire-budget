using System;
using HomeBudget.Common;
using HomeBudget.MaintenanceUseCases;
using HomeBudget.MaintenanceUseCases.Request;
using HomeBudget.MaintenanceUseCases.RequestDto;
using HomeBudget.Repositories;
using NUnit.Framework;

namespace HomeBudget.AcceptanceTest
{
    [TestFixture]
    public class AccountMaintenanceTests
    {
        private AddAccountUseCase _account;
        private InMemoryStore _inMemoryStore;
        private const string AccountName = "MyAccount";

        [SetUp]
        public void SetupAccount()
        {
            Initialise();
            MaintenanceEvents.OnAccountInfoBroadcast += MaintenanceEventsOnAccountInfoBroadcast;
            GivenAccountWithNameAndBalance(AccountName, 1000M);
        }

        void MaintenanceEventsOnAccountInfoBroadcast(AccountInfoDto accountInfo)
        {
            throw new NotImplementedException();
        }

        [Test]
    [Ignore]
        public void Ttttt()
        {
            WhenIAddAOneOffIncomeOf(500M, DateTime.Now.AddDays(2));
            ThenBalanceNowHasNotChanged();
        }

        private void ThenBalanceNowHasNotChanged()
        {
        }

        private void WhenIAddAOneOffIncomeOf(decimal value, DateTime date)
        {
            var req = new MaintenanceRequest<ITransactionDto>(new OneOffTransactionDto(AccountName, value, date));
            new AddTransactionUseCase(_inMemoryStore).Execute(req);
        }

        private void GivenAccountWithNameAndBalance(string accountName, decimal balance)
        {
            _account.Execute(new MaintenanceRequest<AccountDto>(new AccountDto(accountName, balance)));
        }

        private void Initialise()
        {
            _inMemoryStore = new InMemoryStore();
            _account = new AddAccountUseCase(_inMemoryStore);
        }
    }

}

