using System;
using FluentAssertions;
using HomeBudget.Model;
using NUnit.Framework;

namespace HomeBudget.ModelTest
{
    [TestFixture]
    public class OneOffIncomeTest
    {
        private Budget _budget;

        [SetUp]
        public void SetUp()
        {
            _budget = new Budget(100);
        }

        [Test]
        public void ZeroBalance_ZeroPayment_BalanceStaysSame()
        {
            _budget.AddIncome(new Income(0, null, DateTime.Now));

            var balance = _budget.GetBalanceAtDate(DateTime.Now);

            balance.Should().Be(100);
        }
    
        [Test]
        public void ZeroBalance_Payment_BalanceIncreases()
        {
            _budget.AddIncome(new Income(200, null, DateTime.Now));

            var balance = _budget.GetBalanceAtDate(DateTime.Now.AddYears(1));

            balance.Should().Be(300);
        }
        
    }
}