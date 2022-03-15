using Tanks.Core.Infrastructure.StateMachine.Interfaces;
using UnityEngine;

namespace Tanks.Core.Infrastructure.StateMachine.Camera
{
    public sealed class SpeedUpState : IUpdateCameraState, ILoadingState<Transform>
    {
        private const float c_lerpSpeed = 0.1f;
        private readonly CameraStateMachine _stateMachine;
        private Transform _target;
        
        public SpeedUpState(CameraStateMachine stateMachine) => _stateMachine = stateMachine;

        public void Enter(Transform target) => _target = target;

        public void Update()
        {
            _stateMachine.MoveCameraLerp(_target, c_lerpSpeed);
            if (Mathf.Abs(((_target.position - _stateMachine.Camera.position) - _stateMachine.Camera.forward).x) < 0.05f)
                _stateMachine.Follow(_target); 
        }
        
        public void Exit() {}
    }
}