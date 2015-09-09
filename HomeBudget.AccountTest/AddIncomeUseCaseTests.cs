using System;
using System.Collections.Generic;
using FluentAssertions;
using HomeBudget.Common;
using HomeBudget.MaintenanceUseCases;
using HomeBudget.MaintenanceUseCases.Request;
using HomeBudget.MaintenanceUseCases.RequestDto;
using NUnit.Framework;

namespace HomeBudget.RequestTest
{
    [TestFixture]
    public class AddIncomeUseCaseTests : MaintenanceUseCaseTests
    {
        private IUseCase _useCase;
        private int _transactionEventReceivedCount;

        [SetUp]
        public void AddIncomeSetUp()
        {
            _useCase = new AddTransactionUseCase(Repos);
            _transactionEventReceivedCount = 0;
            MaintenanceEvents.OnTransactionInfoBroadcast += MaintenanceEvents_OnTransactionInfoBroadcast;
        }

        [TearDown]
        public void TearDown()
        {
            MaintenanceEvents.OnTransactionInfoBroadcast -= MaintenanceEvents_OnTransactionInfoBroadcast;
        }

        [TestCase(0)]
        [TestCase(10)]
        public void AddIncome_OneOff(decimal val)
        {
            var income = new OneOffTransactionDto("account1", val, DateTime.Now.Date);
            
            ExecuteRequest(income);
            AssertValueReceived("account1", val);
        }

        [Test]
        public void AddIncome_Weekly()
        {
            var income = new WeeklyTransactionDto("account1", 100M, DateTime.Now.Date);
            ExecuteRequest(income);

            AssertValueReceived("account1", 100M);
        }

        [Test]
        public void AddIncome_Fortnightly()
        {
            var income = new FortnighlyTransactionDto("account1", 100M, DateTime.Now.Date);
            ExecuteRequest(income);

            AssertValueReceived("account1", 100M);
        }

        [Test]
        public void AddIncome_Monthly()
        {
            var income = new MonthlyTransactionDto("account1", 100M, DateTime.Now.Date);
            ExecuteRequest(income);

            AssertValueReceived("account1", 100M);
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
            _transactionEventReceivedCount.Should().Be(1);
        }
        private void MaintenanceEvents_OnTransactionInfoBroadcast(TransactionInfoDto transactioninfo)
        {
            _transactionEventReceivedCount++;
        }
    }
}