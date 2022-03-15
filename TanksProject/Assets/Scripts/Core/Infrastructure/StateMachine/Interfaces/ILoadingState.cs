namespace Tanks.Core.Infrastructure.StateMachine.Interfaces
{
    public interface ILoadingState<TLoad> : IExitableState
    {
        void Enter(TLoad load);
    }
}