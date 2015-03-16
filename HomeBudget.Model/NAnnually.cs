using System;

namespace HomeBudget.Model
{
    public class NAnnually : ITerm
    {
        private readonly int _n;

        public NAnnually(int n)
        {
            _n = n;
        }

        public DateTime AddTerm(DateTime toThis)
        {
            return toThis.AddYears(_n);
        }

        public bool IsMatchingDate(DateTime referenceDate, DateTime targetDate)
        {
            if (referenceDate.Day != targetDate.Day || referenceDate.Month != targetDate.Month)
                return false;

            DateTime dt = referenceDate.Date;
            while (targetDate.Date >= dt)
            {
                if (dt == targetDate.Date)
                    return true;
                dt = AddTerm(dt);
            }
            return false;
        }
    }
}