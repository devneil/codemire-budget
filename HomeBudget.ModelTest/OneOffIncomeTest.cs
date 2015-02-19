using System;
using FluentAssertions;
using HomeBudget.Model;
using NUnit.Framework;

namespace HomeBudget.ModelTest
{
    [TestFixture]
    public class OneOffIncomeTest
    {
        private Account _account;

        [SetUp]
        public void SetUp()
        {
            _account = new Account(100);
        }

        [Test]
        public void ZeroBalance_ZeroPayment_BalanceStaysSame()
        {
            _account.AddIncome(new Income(0, DateTime.Now));

            var balance = _account.GetBalanceAtDate(DateTime.Now);

            balance.Should().Be(100);
        }
    
        [Test]
        public void ZeroBalance_Payment_BalanceIncreases()
        {
            _account.AddIncome(new Income(200, DateTime.Now));

            var balance = _account.GetBalanceAtDate(DateTime.Now.AddYears(1));

            balance.Should().Be(300);
        }
        
    }
}