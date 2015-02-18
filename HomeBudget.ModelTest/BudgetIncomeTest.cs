using System;
using FluentAssertions;
using HomeBudget.Model;
using NUnit.Framework;

namespace HomeBudget.ModelTest
{
    [TestFixture]
    public class BudgetIncomeTest
    {
        private Budget _budget;

        [Test]
        public void CombinedIncomes()
        {
            _budget = new Budget();

            _budget.AddIncome(new Income(100, Term.OneOff, DateTime.Now));
            _budget.AddIncome(new Income(500, Term.Monthly, DateTime.Now.AddDays(1)));
            _budget.AddIncome(new Income(200, Term.Weekly, DateTime.Now.AddDays(3)));
            _budget.AddIncome(new Income(300, Term.Fortnightly, DateTime.Now.AddDays(10)));

            BalanceAfterDays(0, 100);
            BalanceAfterDays(1, 600);
            BalanceAfterDays(2, 600);
            BalanceAfterDays(3, 800);
            BalanceAfterDays(4, 800);
            BalanceAfterDays(9, 800);
            BalanceAfterDays(10, 1300);
        }

        private void BalanceAfterDays(int days, int expected)
        {
            _budget.GetBalanceAtDate(DateTime.Now.AddDays(days)).Should().Be(expected);
        }
    }
}