using System;
using HomeBudget.Common;

namespace HomeBudget.MaintenanceUseCases.RequestDto
{
    public class MonthlyTransactionDto : ITransactionDto
    {
        public string AccountName { get; private set; }
        public decimal Value { get; private set; }
        public DateTime Date { get; private set; }

        public MonthlyTransactionDto(string accountName, decimal value, DateTime date)
        {
            AccountName = accountName;
            Value = value;
            Date = date;
        }
    }
}