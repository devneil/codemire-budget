using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HomeBudget.Model
{
    public class Budget
    {
        private readonly List<Account> _accounts = new List<Account>();

        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }

        public ICollection Accounts
        {
            get { return _accounts; }
        }

        public decimal GetPosition(DateTime atThisDate)
        {
            return _accounts.Sum(account => account.GetBalanceAtDate(atThisDate));
        }

        public decimal GetPosition()
        {
            return GetPosition(DateTime.Now);
        }

        public AccountDataPoints GetBalanceRange(DateTime startDate, DateTime endDate)
        {
            var points = new AccountDataPoints();

            foreach (var account in _accounts)
            {
                points.AddPoints(account.AccountName, account.GetBalanceRange(startDate, endDate));
            }
            return points;
        }
    }
}