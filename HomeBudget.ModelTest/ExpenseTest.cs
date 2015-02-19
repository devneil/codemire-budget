using System;
using FluentAssertions;
using HomeBudget.Model;
using NUnit.Framework;

namespace HomeBudget.ModelTest
{
    [TestFixture]
    public class ExpenseTest
    {
        private Account _account;

        [SetUp]
        public void SetUp()
        {
            _account = new Account(1000);
        }

        [Test]
        public void ExpenseReducesBudget()
        {
            _account.AddExpense(new Expense(50, DateTime.Now));

            decimal balance = _account.GetBalanceAtDate(DateTime.Now);

            balance.Should().Be(950);
        }

        [Test]
        public void ExpenseDoesntPayTillDate()
        {
            _account.AddExpense(new Expense(50, DateTime.Now.AddDays(1)));

            decimal balance = _account.GetBalanceAtDate(DateTime.Now);

            balance.Should().Be(1000);
        }

        [Test]
        public void CombinedIncomes()
        {
            _account.AddExpense(new Expense(100, DateTime.Now));
            _account.AddExpense(new Expense(500, DateTime.Now.AddDays(1), new Monthly()));
            _account.AddExpense(new Expense(200, DateTime.Now.AddDays(3), new Weekly()));
            _account.AddExpense(new Expense(300, DateTime.Now.AddDays(10), new Fortnightly()));

            BalanceAfterDays(0, 900);
            BalanceAfterDays(1, 400);
            BalanceAfterDays(2, 400);
            BalanceAfterDays(3, 200);
            BalanceAfterDays(4, 200);
            BalanceAfterDays(9, 200);
            BalanceAfterDays(10, -300);
        }

        private void BalanceAfterDays(int days, int expected)
        {
            _account.GetBalanceAtDate(DateTime.Now.AddDays(days)).Should().Be(expected);
        }
    }
}