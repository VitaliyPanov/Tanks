using Tanks.Core.Infrastructure.StateMachine.Interfaces;
using UnityEngine;

namespace Tanks.Core.Infrastructure.StateMachine
{
    public sealed class DefaultState : IState, IUpdateCameraState
    {
        public void Exit(){}
        public void Enter(){}

        public void Update(){}
    }
}