using System;

namespace HomeBudget.Model
{
    public class Monthly : ITerm
    {
        public DateTime AddTerm(DateTime toThis)
        {
            return toThis.AddMonths(1);
        }
    }
}