using System;

namespace HomeBudget.Model
{
    public class Annually : ITerm
{
        public DateTime AddTerm(DateTime toThis)
        {
            return toThis.AddYears(1);
        }

        public bool IsMatchingDate(DateTime referenceDate, DateTime targetDate)
        {
            return (referenceDate.Day == targetDate.Day && referenceDate.Month == targetDate.Month);
        }
    }
}