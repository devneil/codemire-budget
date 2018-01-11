namespace HomeBudget.Common
{
    public interface IUseCase
    {
        void Execute(IRequest request);
    }
}