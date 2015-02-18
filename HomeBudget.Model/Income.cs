using System;

namespace HomeBudget.Model
{
    public class Income
    {
        public decimal NetValue { get; private set; }
        public Term PerTerm { get; private set; }
        public DateTime NextPayDate { get; private set; }

        public Income(decimal netValue, Term perTerm, DateTime nextPayDate)
        {
            NetValue = netValue;
            PerTerm = perTerm;
            NextPayDate = nextPayDate.Date;
        }
    }
}