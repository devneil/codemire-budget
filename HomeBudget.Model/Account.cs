using System;
using System.Collections.Generic;

namespace HomeBudget.Model
{
    public class Account
    {
        private readonly List<ITransaction> _transactions = new List<ITransaction>();
        private decimal _balance;
        
        public Account(string name)
        {
            AccountName = name;
            _balance = 0;
        }

        public Account(string name, decimal balance)
        {
            AccountName = name;
            _balance = balance;
        }

        public string AccountName { get; private set; }

        public void AddIncome(Income income)
        {
            _transactions.Add(income);
        }

        public decimal GetBalanceAtDate(DateTime atDate)
        {
            decimal balance = _balance;
            balance = SumTransactionsToDate(atDate, balance);
            return balance;
        }

        private decimal SumTransactionsToDate(DateTime atDate, decimal balance)
        {
            foreach (var tran in _transactions)
            {
                DateTime newDate = tran.OnDate;

                if (tran.PerTerm == null)
                {
                    if (atDate.Date >= newDate)
                    {
                        balance += tran.CalcValue;
                    }
                }
                else
                {
                    while (atDate.Date >= newDate)
                    {
                        balance += tran.CalcValue;
                        newDate = tran.PerTerm.AddTerm(newDate);
                    }
                }
            }
            return balance;
        }

        public void AddExpense(Expense expense)
        {
            _transactions.Add(expense);
        }

        public DataPoints GetBalanceRange(DateTime startDate, DateTime endDate)
        {
            var pts = new DataPoints();
            for (DateTime dt = startDate.Date; dt <= endDate.Date; dt = dt.AddDays(1))
            {
                pts.AddPoint(dt, GetBalanceAtDate(dt));
            }
            return pts;
        }
    }
}