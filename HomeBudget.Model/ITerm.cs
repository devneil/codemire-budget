using System;

namespace HomeBudget.Model
{
    public interface ITerm
    {
        DateTime AddTerm(DateTime toThis);
    }

    public interface ITransaction
    {
        Decimal CalcValue { get; }
        DateTime OnDate { get; }
        ITerm PerTerm { get; }
    }
}