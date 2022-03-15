using Tanks.Core.Infrastructure.StateMachine.Interfaces;
using UnityEngine;

namespace Tanks.Core.Infrastructure.StateMachine.Camera
{
    public sealed class SwitchTargetState : IUpdateCameraState, ILoadingState<Transform>
    {
        private readonly CameraStateMachine _stateMachine;
        private Transform _target;
        
        public SwitchTargetState(CameraStateMachine stateMachine) => _stateMachine = stateMachine;

        public void Enter(Transform target) => _target = target;

        public void Update()
        {
            _stateMachine.MoveCameraLerp(_target,Time.deltaTime);
            if (Mathf.Abs(((_target.position - _stateMachine.Camera.position) - _stateMachine.Camera.forward).x) < 1f)
                _stateMachine.SpeedUp(_target);
        }
        public void Exit() {}
    }
}