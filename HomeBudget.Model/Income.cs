using System;

namespace HomeBudget.Model
{
    public class Income : Transaction
    {
        public Income(decimal netValue, DateTime nextPayDate, ITerm perTerm) : base(netValue, nextPayDate, perTerm)
        {
        }

        public Income(decimal netValue, DateTime nextPayDate) : base(netValue, nextPayDate)
        {
        }

        public override decimal CalcValue
        {
            get { return NetValue; }
        }
    }
}