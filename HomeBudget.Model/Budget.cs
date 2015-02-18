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

                while (atDate.Date >= newDate)
                {
                    balance += income.NetValue;
                    switch (income.PerTerm)
                    {
                        case Term.Monthly:
                            newDate = newDate.AddMonths(1);
                            break;
                        case Term.Fortnightly:
                            newDate = newDate.AddDays(14);
                            break;
                        case Term.Weekly:
                            newDate = newDate.AddDays(7);
                            break;
                        case Term.OneOff:
                            newDate = atDate.AddDays(1);
                            break;
                    }
                }
            }
            return balance;
        }
    }
}