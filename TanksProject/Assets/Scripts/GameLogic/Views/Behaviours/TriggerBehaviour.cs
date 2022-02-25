using Entitas;
using UnityEngine;

namespace Tanks.GameLogic.Views.Behaviours
{
    [RequireComponent(typeof(UnityView))]
    public class TriggerBehaviour : MonoBehaviour, IBehaviour
    {
        private GameEntity _gameEntity;

        public void Initialize(IEntity entity) => _gameEntity = (GameEntity) entity;

        private void OnTriggerEnter(Collider other)
        {
            _gameEntity.ReplacePosition(transform.position);
            _gameEntity.isTriggered = true;
        }
    }
}