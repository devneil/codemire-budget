using System;
using FluentAssertions;
using HomeBudget.Model;
using NUnit.Framework;

namespace HomeBudget.ModelTest
{
    [TestFixture]
    public class OneOffIncomeTest
    {
        [Test]
        public void ZeroBalance_ZeroPayment_BalanceStaysSame()
        {
            var budget = new Budget(100);
            budget.AddIncome(new Income(0, Term.OneOff, DateTime.Now));

            var balance = budget.GetBalanceAtDate(DateTime.Now);

            balance.Should().Be(100);
        }
    
        [Test]
        public void ZeroBalance_Payment_BalanceIncreases()
        {
            var budget = new Budget(100);
            budget.AddIncome(new Income(200, Term.OneOff, DateTime.Now));

            var balance = budget.GetBalanceAtDate(DateTime.Now.AddYears(1));

            balance.Should().Be(300);
        }


    }
}