using System;

namespace HomeBudget.Model
{
    public class NMonthly : ITerm
    {
        private readonly int _n;

        public NMonthly(int n)
        {
            _n = n;
        }

        public DateTime AddTerm(DateTime toThis)
        {
            return toThis.AddMonths(_n);
        }

        public bool IsMatchingDate(DateTime referenceDate, DateTime targetDate)
        {
            if (referenceDate.Day != targetDate.Day)
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