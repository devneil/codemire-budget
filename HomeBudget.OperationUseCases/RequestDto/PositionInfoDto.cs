using System;

namespace HomeBudget.OperationUseCases
{
    public class PositionInfoDto
    {
        public decimal Position { get; set; }
        public DateTime Date { get; set; }

        public PositionInfoDto(decimal position, DateTime date)
        {
            Position = position;
            Date = date;
        }
    }
}