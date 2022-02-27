using Entitas;
using Tanks.GameLogic.Services;
using UnityEngine;
using UnityEngine.AI;

namespace Tanks.GameLogic.Views.Behaviours
{
    [RequireComponent(typeof(UnityView))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class NavMeshAgentBehaviour : MonoBehaviour, IBehaviour
    {
        private GameEntity _gameEntity;
        private AIEntity _aiEntity;
        private int _movableComponent;

        public void Initialize(IEntity entity)
        {
            switch (entity)
            {
                case GameEntity gameEntity:
                    _gameEntity = gameEntity;
                    _gameEntity.ReplaceAI(this);
                    _gameEntity.OnComponentAdded += ToggleActivity;
                    _gameEntity.OnComponentRemoved += ToggleActivity;
                    _movableComponent = GameComponentsLookup.Movable;
                    break;
                case AIEntity aiEntity:
                    _aiEntity = aiEntity;
                    var agent = gameObject.GetOrAddComponent<NavMeshAgent>();
                    _aiEntity.AddNavMesh(agent);
                    agent.enabled = false;
                    break;
            }
        }

        private void ToggleActivity(IEntity entity, int index, IComponent component)
        {
            if (_aiEntity == null) return;
            if (index == _movableComponent)
            {
                _aiEntity.isActive = _gameEntity.isMovable;
            }
        }

        private void OnDestroy()
        {
            _gameEntity.OnComponentAdded -= ToggleActivity;
            _gameEntity.OnComponentRemoved -= ToggleActivity;
        }
    }
}