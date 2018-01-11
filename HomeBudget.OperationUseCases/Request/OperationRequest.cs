using HomeBudget.Common;

namespace HomeBudget.OperationUseCases.Request
{
    public class OperationRequest<T> : IRequest
    {
        public T Dto { get; set; }

        public OperationRequest(T dto)
        {
            Dto = dto;
        }
    }
}