using System;

namespace HomeBudget.Model
{
    public class Income : ITransaction
    {
        public decimal NetValue { get; private set; }
        public ITerm PerTerm { get; private set; }
        public DateTime OnDate { get; private set; }
        public Decimal CalcValue { get { return NetValue; } }

        public Income(decimal netValue, DateTime nextPayDate, ITerm perTerm)
        {
            NetValue = netValue;
            PerTerm = perTerm;
            OnDate = nextPayDate.Date;
        }

        public Income(decimal netValue, DateTime nextPayDate)
        {
            NetValue = netValue;
            PerTerm = null;
            OnDate = nextPayDate.Date;
        }

        
    }
}