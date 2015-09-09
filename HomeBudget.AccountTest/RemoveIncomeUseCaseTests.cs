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
    public class RemoveIncomeUseCaseTests : MaintenanceUseCaseTests
    {
        private RemoveTransactionUseCase _useCase;
        private TransactionInfoDto _removeTransactionReceived;

        [SetUp]
        public void RemoveIncomeSetUp()
        {
            _useCase = new RemoveTransactionUseCase(Repos);
            MaintenanceEvents.OnRemoveTransactionBroadcast += MaintenanceEvents_OnRemoveTransactionBroadcast;
        }

        [TearDown]
        public void TearDown()
        {
            MaintenanceEvents.OnRemoveTransactionBroadcast -= MaintenanceEvents_OnRemoveTransactionBroadcast;
        }
        
        [Test]
        public void RemoveIncome()
        {
            var req = new MaintenanceRequest<ITransactionDto>(new WeeklyTransactionDto("account", 100M, DateTime.Now));
            _useCase = new RemoveTransactionUseCase(Repos);

            _useCase.Execute(req);

            Repos.RemoveTransactionCalled.Should().BeTrue();
            Repos.RemoveAccountName.Should().Be("account");
            _removeTransactionReceived.AccountName.Should().Be("account");
            _removeTransactionReceived.Value.Should().Be(100M);
            _removeTransactionReceived.Date.Should().Be(DateTime.Now.Date);
        }
    
        void MaintenanceEvents_OnRemoveTransactionBroadcast(TransactionInfoDto transactionInfo)
        {
            _removeTransactionReceived = transactionInfo;
        }
    }
}