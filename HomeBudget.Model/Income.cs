using System;

namespace HomeBudget.Model
{
    public class Income
    {
        public decimal NetValue { get; private set; }
        public ITerm PerTerm { get; private set; }
        public DateTime NextPayDate { get; private set; }

        public Income(decimal netValue, ITerm perTerm, DateTime nextPayDate)
        {
            NetValue = netValue;
            PerTerm = perTerm;
            NextPayDate = nextPayDate.Date;
        }

    }
}