using System;

namespace HomeBudget.Model
{
    public abstract class Transaction
    {
        public decimal NetValue { get; private set; }
        public ITerm PerTerm { get; private set; }
        public DateTime OnDate { get; private set; }
        public abstract Decimal CalcValue { get; }

        protected Transaction(decimal netValue, DateTime nextPayDate, ITerm perTerm)
        {
            NetValue = netValue;
            PerTerm = perTerm;
            OnDate = nextPayDate.Date;
        }

        protected Transaction(decimal netValue, DateTime nextPayDate)
        {
            NetValue = netValue;
            PerTerm = null;
            OnDate = nextPayDate.Date;
        }

        public bool IsPayDate(DateTime date)
        {
            if (PerTerm == null)
                return (OnDate == date.Date);
            return PerTerm.IsMatchingDate(OnDate, date);
        }

    }
}