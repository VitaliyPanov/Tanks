namespace Tanks.Core.Infrastructure.StateMachine.Interfaces
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}