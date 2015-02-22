using System.Collections.Generic;
using System.Linq;

namespace HomeBudget.Model
{
    public class AccountDataPoints
    {
        private readonly Dictionary<string, DataPoints> _accountData = new Dictionary<string, DataPoints>();
        public int AccountCount { get { return _accountData.Keys.Count; } }
        public int DateCount { get { return _accountData.Values.First().PointCount; } }

        public DataPoints this[string accountName]
        {
            get { return _accountData[accountName]; }
        }

        public void AddPoints(string accountName, DataPoints dataPoints)
        {
            _accountData[accountName] = dataPoints;
        }
    }
}