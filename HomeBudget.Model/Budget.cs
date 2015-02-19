using System;
using System.Collections.Generic;

namespace HomeBudget.Model
{
    public class Budget
    {
        private readonly List<ITransaction> _transactions = new List<ITransaction>();
        private decimal _balance;
        
        public Budget()
        {
            _balance = 0;
        }

        public Budget(decimal balance)
        {
            _balance = balance;
        }

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
    }
}