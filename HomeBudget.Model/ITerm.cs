using System;

namespace HomeBudget.Model
{
    public interface ITerm
    {
        DateTime AddTerm(DateTime toThis);
        bool IsMatchingDate(DateTime referenceDate, DateTime targetDate);
    }
}