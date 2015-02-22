using System;

namespace HomeBudget.Model
{
    public class Weekly : ITerm
    {
        public DateTime AddTerm(DateTime toThis)
        {
            return toThis.AddDays(7);
        }

        public bool IsMatchingDate(DateTime referenceDate, DateTime targetDate)
        {
            return (targetDate.Subtract(referenceDate).Days%7 == 0);
        }
    }
}