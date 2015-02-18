using System;
using FluentAssertions;
using HomeBudget.Model;
using NUnit.Framework;

namespace HomeBudget.ModelTest
{
    [TestFixture]
    public class FortnightlyIncomeTest
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
            _budget.AddIncome(new Income(0, Term.Fortnightly, DateTime.Now));

            decimal balance = _budget.GetBalanceAtDate(DateTime.Now.AddMonths(3));

            balance.Should().Be(0);
        }

        [TestCase(0, 100)]
        [TestCase(1, 100)]
        [TestCase(2, 200)]
        [TestCase(3, 200)]
        [TestCase(4, 300)]
        [TestCase(5, 300)]
        [TestCase(6, 400)]
        [TestCase(7, 400)]
        [TestCase(8, 500)]
        public void PaydayFortnightlyIncomeContinuallyPaid(int weeks, decimal expected)
        {
            _budget.AddIncome(new Income(100, Term.Fortnightly, DateTime.Now));

            decimal balance = _budget.GetBalanceAtDate(DateTime.Now.AddDays(7 * weeks));

            balance.Should().Be(expected);
        }

        [TestCase(0, 0)]
        [TestCase(1, 100)]
        [TestCase(2, 100)]
        [TestCase(3, 200)]
        [TestCase(4, 200)]
        public void BeforePaydayFortnightlyIncomeContinuallyPaid(int weeks, decimal expected)
        {
            _budget.AddIncome(new Income(100, Term.Fortnightly, DateTime.Now.AddDays(2)));

            decimal balance = _budget.GetBalanceAtDate(DateTime.Now.AddDays(7*weeks));

            balance.Should().Be(expected);
        }

    }
}