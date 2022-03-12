using System;
using Entitas;
using UnityEngine;

namespace Tanks.GameLogic.Views.Behaviours
{
    [RequireComponent(typeof(UnityView))]
    public sealed class TriggerBehaviour : MonoBehaviour, IBehaviour
    {
        private GameEntity _gameEntity;

        public void Construct(IEntity entity)
        {
            if (entity is GameEntity gameEntity)
                _gameEntity = gameEntity;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_gameEntity == null) return;
            _gameEntity.ReplacePosition(transform.position);
            _gameEntity.isTriggered = true;
        }
    }
}