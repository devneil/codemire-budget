using System;
using HomeBudget.Common;

namespace HomeBudget.MaintenanceUseCases.RequestDto
{
    public class WeeklyTransactionDto : ITransactionDto
    {
        public DateTime Date { get; private set; }
        public string AccountName { get; set; }
        public decimal Value { get; private set; }

        public WeeklyTransactionDto(string accountName, decimal value, DateTime date)
        {
            Date = date.Date;
            AccountName = accountName;
            Value = value;
        }

        
    }
}