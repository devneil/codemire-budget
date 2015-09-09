using System;
using FluentAssertions;
using HomeBudget.Common;
using HomeBudget.MaintenanceUseCases;
using HomeBudget.MaintenanceUseCases.Request;
using HomeBudget.MaintenanceUseCases.RequestDto;
using NUnit.Framework;

namespace HomeBudget.RequestTest
{
    [TestFixture]
    public class AddExpenseUseCaseTests : MaintenanceUseCaseTests
    {
        private IUseCase _useCase;
        private TransactionInfoDto _transactionEventReceived;

        [SetUp]
        public void AddIncomeSetUp()
        {
            _useCase = new AddTransactionUseCase(Repos);
            _transactionEventReceived = null;
            MaintenanceEvents.OnTransactionInfoBroadcast += MaintenanceEvents_OnTransactionInfoBroadcast;
        }

        [TearDown]
        public void TearDown()
        {
            MaintenanceEvents.OnTransactionInfoBroadcast -= MaintenanceEvents_OnTransactionInfoBroadcast;
        }

        [TestCase(0)]
        [TestCase(-10)]
        public void AddExpense_OneOff(decimal val)
        {
            var income = new OneOffTransactionDto("account1", val, DateTime.Now.Date);
            
            
            ExecuteRequest(income);
            AssertValueReceived("account1", val);
        }

        [Test]
        public void AddExpense_Weekly()
        {
            var income = new WeeklyTransactionDto("account1", -100M, DateTime.Now.Date);
            ExecuteRequest(income);

            AssertValueReceived("account1", -100M);
        }

        [Test]
        public void AddExpense_Fortnightly()
        {
            var income = new FortnighlyTransactionDto("account1", -100M, DateTime.Now.Date);
            ExecuteRequest(income);

            AssertValueReceived("account1", -100M);
        }

        [Test]
        public void AddExpense_Monthly()
        {
            var income = new MonthlyTransactionDto("account1", -100M, DateTime.Now.Date);
            ExecuteRequest(income);

            AssertValueReceived("account1", -100M);
        }

        private void ExecuteRequest(ITransactionDto transaction)
        {
            var req = new MaintenanceRequest<ITransactionDto>(transaction);

            _useCase.Execute(req);
        }

        private void AssertValueReceived(string account, decimal expected)
        {
            Repos.AddTranCalled.Should().BeTrue();
            Repos.AddTranValue.Should().Be(expected);
            Repos.AddTranDate.Should().Be(DateTime.Now.Date);
            Repos.AddTranAccount.Should().Be(account);
            _transactionEventReceived.AccountName.Should().Be(account);
            _transactionEventReceived.Value.Should().Be(expected);
        }

        private void MaintenanceEvents_OnTransactionInfoBroadcast(TransactionInfoDto transactioninfo)
        {
            _transactionEventReceived = transactioninfo;
        }

    }
}