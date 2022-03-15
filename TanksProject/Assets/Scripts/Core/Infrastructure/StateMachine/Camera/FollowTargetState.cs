using Tanks.Core.Infrastructure.StateMachine.Interfaces;
using UnityEngine;

namespace Tanks.Core.Infrastructure.StateMachine.Camera
{
    public sealed class FollowTargetState : IUpdateCameraState, ILoadingState<Transform>
    {
        private readonly CameraStateMachine _stateMachine;
        private Transform _target;

        public FollowTargetState(CameraStateMachine stateMachine) => _stateMachine = stateMachine;

        public void Enter(Transform target) => _target = target;

        public void Update() => _stateMachine.MoveCamera(_target);

        public void Exit()
        {

        }
    }
}