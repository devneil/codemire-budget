﻿using System;
using FluentAssertions;
using HomeBudget.Model;
using NUnit.Framework;

namespace HomeBudget.ModelTest
{
    [TestFixture]
    public class WeeklyIncomeTest
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
            _budget.AddIncome(new Income(0, Term.Weekly, DateTime.Now));

            decimal balance = _budget.GetBalanceAtDate(DateTime.Now.AddMonths(3));

            balance.Should().Be(0);
        }

        [TestCase(0, 100)]
        [TestCase(1, 200)]
        [TestCase(2, 300)]
        [TestCase(3, 400)]
        [TestCase(4, 500)]
        [TestCase(5, 600)]
        [TestCase(6, 700)]
        [TestCase(7, 800)]
        [TestCase(8, 900)]
        public void PaydayWeeklyIncomeContinuallyPaid(int weeks, decimal expected)
        {
            _budget.AddIncome(new Income(100, Term.Weekly, DateTime.Now));

            decimal balance = _budget.GetBalanceAtDate(DateTime.Now.AddDays(7 * weeks));

            balance.Should().Be(expected);
        }

        [TestCase(0, 0)]
        [TestCase(1, 100)]
        [TestCase(2, 200)]
        [TestCase(3, 300)]
        [TestCase(4, 400)]
        public void BeforePaydayWeeklyIncomeContinuallyPaid(int weeks, decimal expected)
        {
            _budget.AddIncome(new Income(100, Term.Weekly, DateTime.Now.AddDays(2)));

            decimal balance = _budget.GetBalanceAtDate(DateTime.Now.AddDays(7 * weeks));

            balance.Should().Be(expected);
        }

    }
}