using System;
using FluentAssertions;
using HomeBudget.Model;
using NUnit.Framework;

namespace HomeBudget.ModelTest
{
    [TestFixture]
    public class MonthlyIncomeTest
    {
        private Budget _budget;

        [SetUp]
        public void SetUp()
        {
            _budget = new Budget();
        }

        [Test]
        public void ZeroBalance_ZeroIncome_NoChangeToBalance()
        {
            _budget.AddIncome(new Income(0, Term.Monthly, DateTime.Now));

            DateTime addMonths = DateTime.Now.AddMonths(3);
            decimal balance = _budget.GetBalanceAtDate(addMonths);

            balance.Should().Be(0);
        }

        [Test]
        public void ZeroBalance_NoIncomeSet_NoChangeToBalance()
        {
            decimal balance = _budget.GetBalanceAtDate(DateTime.Now.AddMonths(3));

            balance.Should().Be(0);
        }

        [Test]
        public void ZeroBalance_MonthlyIncomePaid()
        {
            _budget.AddIncome(new Income(100, Term.Monthly, DateTime.Now));

            decimal balance = _budget.GetBalanceAtDate(DateTime.Now);

            balance.Should().Be(100);
        }
        
        [TestCase(1, 200)]
        [TestCase(2, 300)]
        [TestCase(3, 400)]
        [TestCase(4, 500)]
        public void PaydayMonthlyIncomeContinuallyPaid(int months, decimal expected)
        {
            _budget.AddIncome(new Income(100, Term.Monthly, DateTime.Now));

            decimal balance = _budget.GetBalanceAtDate(DateTime.Now.AddMonths(months));

            balance.Should().Be(expected);
        }

        [TestCase(1, 100)]
        [TestCase(2, 200)]
        [TestCase(3, 300)]
        [TestCase(4, 400)]
        public void BeforePaydayMonthlyIncomeContinuallyPaid(int months, decimal expected)
        {
            _budget.AddIncome(new Income(100, Term.Monthly, DateTime.Now.AddDays(2)));

            decimal balance = _budget.GetBalanceAtDate(DateTime.Now.AddMonths(months));

            balance.Should().Be(expected);
        }



    }

}
