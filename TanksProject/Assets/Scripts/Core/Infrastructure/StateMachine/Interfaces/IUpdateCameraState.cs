using UnityEngine;

namespace Tanks.Core.Infrastructure.StateMachine.Interfaces
{
    public interface IUpdateCameraState : IExitableState
    {
        void Update();
    }
}