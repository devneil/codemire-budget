using System;
using System.Collections.Generic;

namespace HomeBudget.Model
{
    public class Account
    {
        private readonly List<Transaction> _transactions = new List<Transaction>();
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

        private DataPoints RunningTotalsBetweenDates(DateTime start, DateTime end)
        {
            var pts = new DataPoints();

            decimal balance = GetBalanceAtDate(start);

            pts.AddPoint(start, balance);

            for (DateTime dt = start.AddDays(1); dt <= end; dt = dt.AddDays(1))
            {
                foreach (var tran in _transactions)
                {
                    if (tran.IsPayDate(dt))
                    {
                        balance += tran.CalcValue;
                    }
                }
                pts.AddPoint(dt, balance);
            }

            return pts;

        }
        public void AddExpense(Expense expense)
        {
            _transactions.Add(expense);
        }

        public DataPoints GetBalanceRange(DateTime startDate, DateTime endDate)
        {
            return RunningTotalsBetweenDates(startDate.Date, endDate.Date);
        }
    }
}