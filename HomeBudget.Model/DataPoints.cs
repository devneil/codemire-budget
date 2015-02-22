using System;
using System.Collections.Generic;

namespace HomeBudget.Model
{
    public class DataPoints
    {
        private readonly Dictionary<DateTime, decimal> _points = new Dictionary<DateTime, decimal>(); 
        public decimal this[DateTime day]
        {
            get { return _points[day.Date]; }
        }

        public int PointCount { get { return _points.Count; } }

        public void AddPoint(DateTime dt, decimal balanceAtDate)
        {
            _points[dt] = balanceAtDate;
        }
    }
}