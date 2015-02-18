using System;

namespace HomeBudget.Model
{
    public class Weekly : ITerm
    {
        public DateTime AddTerm(DateTime toThis)
        {
            return toThis.AddDays(7);
        }
    }
}