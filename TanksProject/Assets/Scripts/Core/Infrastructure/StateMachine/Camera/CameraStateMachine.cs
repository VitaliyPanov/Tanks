using Tanks.Core.Infrastructure.StateMachine.Interfaces;
using UnityEngine;

namespace Tanks.Core.Infrastructure.StateMachine.Camera
{
    public sealed class CameraStateMachine : StateMachine
    {
        public Transform Camera { get; }
        private IUpdateCameraState _updateState;

        private readonly Quaternion _rotation;
        private float _rotationAngleX = 60f;
        private float _distance = 35f;


        public CameraStateMachine(Transform camera)
        {
            Camera = camera;
            States[typeof(SwitchTargetState)] = new SwitchTargetState(this);
            States[typeof(SpeedUpState)] = new SpeedUpState(this);
            States[typeof(FollowTargetState)] = new FollowTargetState(this);
            Enter<DefaultState>();

            _rotation = Quaternion.Euler(_rotationAngleX, 0, 0);
            Camera.rotation = _rotation;
        }

        public void Update()
        {
            _updateState.Update();
        }

        public void MoveCameraLerp(Transform target, float speed) => Camera.position = Vector3.Lerp(Camera.position,
            _rotation * new Vector3(0, 0, -_distance) + target.position, speed);

        public void MoveCamera(Transform target) =>
            Camera.position = _rotation * new Vector3(0, 0, -_distance) + target.position;

        public void SwitchTarget(Transform target) => Enter<SwitchTargetState, Transform>(target);

        public void SpeedUp(Transform target) => Enter<SpeedUpState, Transform>(target);

        public void Follow(Transform target) => Enter<FollowTargetState, Transform>(target);

        private protected override TState ChangeState<TState>()
        {
            var state = base.ChangeState<TState>();
            if (state is IUpdateCameraState updatableState)
                _updateState = updatableState;
            else
                _updateState = GetState<DefaultState>();
            return state;
        }
    }
}