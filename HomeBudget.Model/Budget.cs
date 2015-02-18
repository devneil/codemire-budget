using System;

namespace HomeBudget.Model
{
    public class Budget
    {
        private Income _income;
        private decimal _budget;

        public Budget()
        {
            _budget = 0;
        }

        public Budget(decimal budget)
        {
            _budget = budget;
        }

        public void AddIncome(Income income)
        {
            _income = income;
        }

        public decimal GetBalanceAtDate(DateTime atDate)
        {
            if (_income == null) return _budget;

            DateTime newDate = _income.NextPayDate;

            while (atDate.Date >= newDate)
            {
                _budget += _income.NetValue;
                switch (_income.PerTerm)
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

            return _budget;
        }
    }
}