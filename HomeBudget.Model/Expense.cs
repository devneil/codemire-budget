using System;

namespace HomeBudget.Model
{
    public class Expense : ITransaction
    {
        public decimal Value { get; private set; }
        public ITerm PerTerm { get; private set; }
        public DateTime OnDate { get; private set; }
        public Decimal CalcValue { get { return -(Value); } }

        public Expense(decimal value, DateTime date)
        {
            Value = value;
            PerTerm = null;
            OnDate = date.Date;
        }
        
        public Expense(decimal value, DateTime date, ITerm perTerm)
        {
            Value = value;
            PerTerm = perTerm;
            OnDate = date.Date;
        }
    }
}