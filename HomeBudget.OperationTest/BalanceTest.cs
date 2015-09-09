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
    public class BalanceTest
    {
        private AccountBalanceInfoDto _accBalance;

        [Test]
        public void CanGetBalance()
        {
            OperationEvents.OnAccountBalanceInfoBroadcast += OperationEvents_OnAccountBalanceInfoBroadcast;
            new GetAccountBalanceUseCase(new TestRepository()).Execute(new OperationRequest<string>("Account1"));

            _accBalance.AccountName.Should().Be("Account1");
            _accBalance.Balance.Should().Be(50M);
        }

        private void OperationEvents_OnAccountBalanceInfoBroadcast(AccountBalanceInfoDto accountBalanceInfo)
        {
            _accBalance = accountBalanceInfo;
        }

        private class TestRepository : IOperationRepository
        {
            public decimal GetAccountBalance(string accountName)
            {
                return 50M;
            }

            public DateTime[] GetTransactionDatesTill(DateTime date)
            {
                throw new NotImplementedException();
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
    }
}
