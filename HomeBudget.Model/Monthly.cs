using System;

namespace HomeBudget.Model
{
    public class Monthly : ITerm
    {
        public DateTime AddTerm(DateTime toThis)
        {
            return toThis.AddMonths(1);
        }

        public bool IsMatchingDate(DateTime referenceDate, DateTime targetDate)
        {
            return ((targetDate.Day == referenceDate.Day) && (targetDate >= referenceDate));
        }

    }
}