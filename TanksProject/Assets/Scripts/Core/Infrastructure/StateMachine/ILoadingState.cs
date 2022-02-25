namespace Tanks.Core.Infrastructure.StateMachine
{
    public interface ILoadingState<TLoad> : IExitableState
    {
        void Enter(TLoad load);
    }
}