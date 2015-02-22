using System;
using System.Collections.Generic;
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
            var acc1 = new Account("Account1", 100);
            var acc2 = new Account("Account2", 200);

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

        [Test]
        public void CanGetGraphData()
        {
            AccountDataPoints points = _budget.GetBalanceRange(DateTime.Now, DateTime.Now.AddDays(5));

            points.AccountCount.Should().Be(2);
            points.DateCount.Should().Be(6);
            TestPointValue(points, "Account1",
                new Dictionary<int, decimal> {{0, 100}, {1, 100}, {2, 100}, {3, 150}, {4, 150}, {5, 150}});
            TestPointValue(points, "Account2",
                new Dictionary<int, decimal> {{0, 200}, {1, 200}, {2, 200}, {3, 200}, {4, 280}, {5, 280}});

        }

        private static void TestPointValue(AccountDataPoints points, string accountName, Dictionary<int,decimal> expected)
        {
            foreach (var kvp in expected)
            {
                points[accountName][DateTime.Now.AddDays(kvp.Key)].Should().Be(kvp.Value);    
            }
            
        }
    }


}