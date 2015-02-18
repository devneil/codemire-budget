using System;
using System.Collections.Generic;

namespace HomeBudget.Model
{
    public class Budget
    {
        private readonly List<Income> _incomes = new List<Income>();
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
            _incomes.Add(income);
        }

        public decimal GetBalanceAtDate(DateTime atDate)
        {
            decimal balance = _balance;
            foreach (var income in _incomes)
            {
                DateTime newDate = income.NextPayDate;

                if (income.PerTerm == null)
                {
                    if (atDate.Date >= newDate)
                    {
                        balance += income.NetValue;
                    }
                }
                else
                {
                    while (atDate.Date >= newDate)
                    {
                        balance += income.NetValue;
                        newDate = income.PerTerm.AddTerm(newDate);
                    }
                }
            }
            return balance;
        }
    }
}