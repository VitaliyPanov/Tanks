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
                var agent = gameObject.GetOrAddComponent<NavMeshAgent>();
                _aiEntity.AddNavMesh(agent);
                agent.enabled = false;
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

        private void SetAIActivity(bool active) => _aiEntity.isActive = active;
    }
}