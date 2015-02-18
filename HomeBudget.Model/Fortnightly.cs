using System;

namespace HomeBudget.Model
{
    public class Fortnightly : ITerm
    {
        public DateTime AddTerm(DateTime toThis)
        {
            return toThis.AddDays(14);
        }
    }
}
