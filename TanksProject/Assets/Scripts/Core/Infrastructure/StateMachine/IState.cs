namespace Tanks.Core.Infrastructure.StateMachine
{
    public interface IState:IExitableState
    {
        void Enter();
    }
}