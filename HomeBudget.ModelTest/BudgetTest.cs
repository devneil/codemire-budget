using System;
using FluentAssertions;
using HomeBudget.Model;
using NUnit.Framework;

namespace HomeBudget.ModelTest
{
    [TestFixture]
    public class BudgetTest
    {
        private Budget _budget;

        [SetUp]
        public void SetUp()
        {
            _budget = new Budget();
            var acc1 = new Account(100);
            var acc2 = new Account(200);

            acc1.AddIncome(new Income(50, DateTime.Now.AddDays(3)));
            acc2.AddIncome(new Income(80, DateTime.Now.AddDays(4)));

            _budget.AddAccount(acc1);
            _budget.AddAccount(acc2);
        }

        [Test]
        public void CanAddAccounts()
        {
            _budget.Accounts.Count.Should().Be(2);
        }

        [Test]
        public void CanGetBudgetPosition()
        {
            _budget.GetPosition().Should().Be(300);
        }

        [Test]
        public void CanGetBudgetPositionForDate()
        {
            _budget.GetPosition(DateTime.Now.AddDays(5)).Should().Be(430);
        }
    }
}