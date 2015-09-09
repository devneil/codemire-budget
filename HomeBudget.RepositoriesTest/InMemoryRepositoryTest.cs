using System;
using System.Linq;
using FluentAssertions;
using HomeBudget.MaintenanceUseCases.RequestDto;
using HomeBudget.Repositories;
using NUnit.Framework;

namespace HomeBudget.RepositoriesTest
{
    [TestFixture]
    public class InMemoryRepositoryTest
    {
        private FakeRep _rep;

        [SetUp]
        public void SetUp()
        {
            _rep = new FakeRep();
            _rep.AddAccount(new AccountDto("Name", 0M));
        }

        [Test]
        public void CanAddAccount()
        {
            _rep.GetAccountCount().Should().Be(1);
        }
        
        [Test]
        public void CanAddAccounts()
        {
            _rep.AddAccount(new AccountDto("Name2", 0M));

            _rep.GetAccountCount().Should().Be(2);
        }

        [Test]
        public void CanAddIncome()
        {
            _rep.AddIncome(new WeeklyTransactionDto("Name", 100M, DateTime.Now));

            _rep.GetIncomeCountForAccount("Name").Should().Be(1);
        }
        
        [Test]
        public void CanAddIncomes()
        {
            _rep.AddIncome(new WeeklyTransactionDto("Name", 100M, DateTime.Now));
            _rep.AddIncome(new FortnighlyTransactionDto("Name", 600M, DateTime.Now));

            _rep.GetIncomeCountForAccount("Name").Should().Be(2);
        }

        [Test]
        public void CanAddIncomesDifferentAccounts()
        {
            _rep.AddIncome(new WeeklyTransactionDto("Name", 100M, DateTime.Now));
            _rep.AddIncome(new FortnighlyTransactionDto("Name2", 600M, DateTime.Now));

            _rep.GetIncomeCountForAccount("Name").Should().Be(1);
            _rep.GetIncomeCountForAccount("Name2").Should().Be(1);
        }

        [Test]
        public void CanRemoveAccount()
        {
            _rep.AddAccount(new AccountDto("Name2", 0M));
            _rep.RemoveAccount("Name");

            _rep.GetAccountCount().Should().Be(1);
        }

        [Test]
        public void CanGetZeroBalance()
        {
            var bal =_rep.GetAccountBalance("Name");

            bal.Should().Be(0M);
        }

        [Test]
        public void CanGetNonZeroBalance()
        {
            _rep.AddAccount(new AccountDto("Name2", 100M));
            var bal =_rep.GetAccountBalance("Name2");

            bal.Should().Be(100M);
        }

        [Test]
        public void AccountWithSameNameAddsIncrementer()
        {
            _rep.AddAccount(new AccountDto("Name", 100M));
            _rep.AddAccount(new AccountDto("Name", 100M));
            _rep.AddAccount(new AccountDto("Name", 100M));

            _rep.HasAccount("Name").Should().BeTrue();
            _rep.HasAccount("Name_1").Should().BeTrue();
            _rep.HasAccount("Name_2").Should().BeTrue();
            _rep.HasAccount("Name_3").Should().BeTrue();
            _rep.GetAccountCount("Name").Should().Be(1);
        }

        [Test]
        public void CanRemoveTransactions()
        {
            _rep.AddIncome(new WeeklyTransactionDto("Name", 100M, DateTime.Now));
            _rep.AddIncome(new FortnighlyTransactionDto("Name", 600M, DateTime.Now));

            _rep.RemoveTransaction(new WeeklyTransactionDto("Name", 100M, DateTime.Now));

            _rep.GetIncomeCountForAccount("Name").Should().Be(1);
        }
        class FakeRep : InMemoryStore
        {
            public int GetAccountCount()
            {
                return Accounts.Count;
            }

            public int GetIncomeCountForAccount(string accountName)
            {
                return GetAccountTransactions().Count(t => t.AccountName == accountName);
            }

            public bool HasAccount(string name)
            {
                return GetAccountBalances().ToList().Exists(acc => acc.AccountName == name);
            }

            public int GetAccountCount(string name)
            {
                return GetAccountBalances().Count(acc => acc.AccountName == name);
            }
        }
    }
}
