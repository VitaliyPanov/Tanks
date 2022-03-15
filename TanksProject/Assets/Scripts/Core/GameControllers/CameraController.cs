using Tanks.Core.Infrastructure.StateMachine.Camera;
using Tanks.General.Controllers;
using UnityEngine;

namespace Tanks.Core.GameControllers
{
    internal sealed class CameraController : ICameraController, ILateUpdate
    {
        private CameraStateMachine _stateMachine;
        
        public void Initialize(Camera camera) => _stateMachine = new CameraStateMachine(camera.transform);

        public void LateUpdate() => _stateMachine.Update();

        public void SetTarget(Transform target) => _stateMachine.SwitchTarget(target);
    }
    
}