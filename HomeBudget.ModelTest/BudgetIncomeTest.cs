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

            _budget.AddIncome(new Income(100, DateTime.Now));
            _budget.AddIncome(new Income(500, DateTime.Now.AddDays(1), new Monthly()));
            _budget.AddIncome(new Income(200, DateTime.Now.AddDays(3), new Weekly()));
            _budget.AddIncome(new Income(300, DateTime.Now.AddDays(10), new Fortnightly()));

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