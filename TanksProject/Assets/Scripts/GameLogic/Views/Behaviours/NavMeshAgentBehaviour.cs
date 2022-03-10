using System;
using Entitas;
using Tanks.GameLogic.Services;
using UnityEngine;
using UnityEngine.AI;

namespace Tanks.GameLogic.Views.Behaviours
{
    [RequireComponent(typeof(UnityView))]
    [RequireComponent(typeof(NavMeshAgent))]
    public sealed class NavMeshAgentBehaviour : MonoBehaviour, IBehaviour, IEventListener, IMovableListener, IMovableRemovedListener
    {
        private AIEntity _aiEntity;

        public void Initialize(IEntity entity)
        {
            if (entity is AIEntity aiEntity)
            {
                _aiEntity = aiEntity;
                _aiEntity.AddNavMesh(gameObject.GetOrAddComponent<NavMeshAgent>());
            }

            if (entity is GameEntity gameEntity)
            {
                if (_aiEntity != null)
                {
                    _aiEntity.AddGameEntity(gameEntity);
                }
                else 
                    throw new Exception("AIEntity must be initialized before GameEntity");
            }
        }
        
        public void AddListener(IEntity entity)
        {
            if (entity is GameEntity gameEntity)
            {
                gameEntity.AddMovableListener(this);
                gameEntity.AddMovableRemovedListener(this);
            }
        }

        public void OnMovable(GameEntity entity)
        {
            if (_aiEntity == null) return;
            SetAIActivity(entity.isMovable);
        }

        public void OnMovableRemoved(GameEntity entity)
        {
            if (_aiEntity == null) return;
            SetAIActivity(entity.isMovable);
        }

        private void SetAIActivity(bool active) => _aiEntity.isCanBeActive = active;
    }
}