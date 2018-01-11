using System;
using HomeBudget.Common;
using HomeBudget.OperationUseCases.Request;

namespace HomeBudget.OperationUseCases
{
    public class GetPositionUseCase : IUseCase
    {
        private readonly IOperationRepository _repository;

        public GetPositionUseCase(IOperationRepository repository)
        {
            _repository = repository;
        }

        public void Execute(IRequest request)
        {
            var req = (OperationRequest<DateTime>)request;

            var accounts = _repository.GetAccountBalances();
            var trans = _repository.GetAccountTransactions();
            var pos = Operations.GetPositionAtDate(req.Dto.Date, accounts, trans);
            
            OperationEvents.BroadcastPositionInfo(new PositionInfoDto(pos, req.Dto.Date));
        }
    }
}