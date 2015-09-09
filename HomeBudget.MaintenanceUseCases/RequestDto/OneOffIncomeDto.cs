using System;
using HomeBudget.Common;

namespace HomeBudget.MaintenanceUseCases.RequestDto
{
    public class OneOffTransactionDto : ITransactionDto
    {
        public DateTime Date { get; private set; }
        public string AccountName { get; set; }
        public decimal Value { get; private set; }

        public OneOffTransactionDto(string accountName, decimal value, DateTime payDate)
        {
            Date = payDate.Date;
            AccountName = accountName;
            Value = value;
        }
    }
}