using HomeBudget.Common;

namespace HomeBudget.MaintenanceUseCases.Request
{
    public class MaintenanceRequest<T> : IRequest
    {
        public T Dto { get; set; }

        public MaintenanceRequest(T dto)
        {
            Dto = dto;
        }
    }
}