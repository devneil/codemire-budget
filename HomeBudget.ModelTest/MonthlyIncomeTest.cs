using System;
using FluentAssertions;
using HomeBudget.Model;
using NUnit.Framework;

namespace HomeBudget.ModelTest
{
    [TestFixture]
    public class MonthlyIncomeTest
    {
        private Account _account;

        [SetUp]
        public void SetUp()
        {
            _account = new Account();
        }

        [Test]
        public void ZeroBalance_ZeroIncome_NoChangeToBalance()
        {
            _account.AddIncome(new Income(0, DateTime.Now, new Monthly()));

            DateTime addMonths = DateTime.Now.AddMonths(3);
            decimal balance = _account.GetBalanceAtDate(addMonths);

            balance.Should().Be(0);
        }

        [Test]
        public void ZeroBalance_NoIncomeSet_NoChangeToBalance()
        {
            decimal balance = _account.GetBalanceAtDate(DateTime.Now.AddMonths(3));

            balance.Should().Be(0);
        }

        [Test]
        public void ZeroBalance_MonthlyIncomePaid()
        {
            _account.AddIncome(new Income(100, DateTime.Now, new Monthly()));

            decimal balance = _account.GetBalanceAtDate(DateTime.Now);

            balance.Should().Be(100);
        }
        
        [TestCase(0, 100)]
        [TestCase(1, 200)]
        [TestCase(2, 300)]
        [TestCase(3, 400)]
        [TestCase(4, 500)]
        public void PaydayMonthlyIncomeContinuallyPaid(int months, decimal expected)
        {
            _account.AddIncome(new Income(100, DateTime.Now, new Monthly()));

            decimal balance = _account.GetBalanceAtDate(DateTime.Now.AddMonths(months));

            balance.Should().Be(expected);
        }

        [TestCase(0, 0)]
        [TestCase(1, 100)]
        [TestCase(2, 200)]
        [TestCase(3, 300)]
        [TestCase(4, 400)]
        public void BeforePaydayMonthlyIncomeContinuallyPaid(int months, decimal expected)
        {
            _account.AddIncome(new Income(100, DateTime.Now.AddDays(2), new Monthly()));

            decimal balance = _account.GetBalanceAtDate(DateTime.Now.AddMonths(months));

            balance.Should().Be(expected);
        }



    }

}
