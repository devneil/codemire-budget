using System;
using FluentAssertions;
using HomeBudget.Model;
using NUnit.Framework;

namespace HomeBudget.ModelTest
{
    [TestFixture]
    public class NMonthlyTest{
        private Account _account;

        [SetUp]
        public void SetUp()
        {
            _account = new Account("");
        }
        [Test]
        public void Nmonth()
        {
            _account.AddIncome(new Income(100, DateTime.Now.AddDays(2), new NMonthly(3)));

            TestValue(DateTime.Now, 0);
            TestValue(DateTime.Now.AddDays(2), 100);
            TestValue(DateTime.Now.AddMonths(3), 100);
            TestValue(DateTime.Now.AddMonths(3).AddDays(2), 200);
            TestValue(DateTime.Now.AddMonths(6), 200);
            TestValue(DateTime.Now.AddMonths(6).AddDays(2), 300);
        }

        private void TestValue(DateTime dateTime, int expected)
        {
            _account.GetBalanceAtDate(dateTime).Should().Be(expected);
        }
    }
}