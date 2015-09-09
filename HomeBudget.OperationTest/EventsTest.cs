using System;
using FluentAssertions;
using HomeBudget.Common;
using HomeBudget.OperationUseCases;
using HomeBudget.OperationUseCases.Request;
using HomeBudget.OperationUseCases.RequestDto;
using NUnit.Framework;

namespace HomeBudget.OperationTest
{
    [TestFixture]
    public class EventsTest
    {
        private AccountBalanceInfoDto _accBalance;
        private AccountBalanceInfoDto _accBalance2;

        [Test]
        public void CanFireMultipleEvents()
        {
            OperationEvents.OnAccountBalanceInfoBroadcast += OperationEvents_OnAccountBalanceInfoBroadcast;
            OperationEvents.OnAccountBalanceInfoBroadcast += OperationEvents_OnAccountBalanceInfoBroadcast2;
            new GetAccountBalanceUseCase(new TestRepository()).Execute(new OperationRequest<string>("Account1"));

            _accBalance.AccountName.Should().Be("Account1");
            _accBalance.Balance.Should().Be(50M);
            _accBalance2.Balance.Should().Be(50M);
        }

        public class TestRepository : IOperationRepository
        {
            public decimal GetAccountBalance(string accountName)
            {
                return 50M;
            }

            public ITransactionDto[] GetAccountTransactions()
            {
                throw new NotImplementedException();
            }

            public AccountBalanceInfoDto[] GetAccountBalances()
            {
                throw new NotImplementedException();
            }
        }

        void OperationEvents_OnAccountBalanceInfoBroadcast(AccountBalanceInfoDto accountBalanceInfo)
        {
            _accBalance = accountBalanceInfo;
        }

        void OperationEvents_OnAccountBalanceInfoBroadcast2(AccountBalanceInfoDto accountBalanceInfo)
        {
            _accBalance2 = accountBalanceInfo;
        }
        
    }
}