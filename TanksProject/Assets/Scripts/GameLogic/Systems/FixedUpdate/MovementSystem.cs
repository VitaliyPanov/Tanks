using Entitas;
using Tanks.Data;
using UnityEngine;

namespace Tanks.GameLogic.Systems.FixedUpdate
{
    internal sealed class MovementSystem : IExecuteSystem
    {
        private readonly RuntimeData _runtimeData;
        private readonly InputContext _inputContext;
        private readonly GameContext _gameContext;

        public MovementSystem(Contexts contexts, RuntimeData runtimeData)
        {
            _runtimeData = runtimeData;
            _inputContext = contexts.input;
            _gameContext = contexts.game;
        }

        public void Execute()
        {
            GameEntity selectedEntity = _gameContext.controllable.Entity;
            Vector2 inputDirection = _inputContext.direction.Value;
            Vector3 deltaPosition = selectedEntity.transform.Value.forward * inputDirection.y *
                                    _runtimeData.MovementSpeed * _inputContext.fixedDeltaTime.Value;
            Quaternion deltaRotation = Quaternion.Euler(0f,
                inputDirection.x * _runtimeData.TurnSpeed * _inputContext.fixedDeltaTime.Value, 0f);

            Move(selectedEntity.rigidbody.Value, deltaPosition, deltaRotation);
        }

        private static void Move(Rigidbody rigidbody, Vector3 deltaPosition, Quaternion deltaRotation)
        {
            rigidbody.MovePosition(rigidbody.position + deltaPosition);
            rigidbody.MoveRotation(rigidbody.rotation * deltaRotation);
        }
    }
}