using System;

namespace HomeBudget.Model
{
    public class Expense : Transaction
    {
        public Expense(decimal netValue, DateTime nextPayDate, ITerm perTerm) : base(netValue, nextPayDate, perTerm)
        {
        }

        public Expense(decimal netValue, DateTime nextPayDate) : base(netValue, nextPayDate)
        {
        }

        public override decimal CalcValue
        {
            get { return -(NetValue); }
        }
    }
}