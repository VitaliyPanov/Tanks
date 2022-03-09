using System.Collections.Generic;
using Entitas;
using Tanks.GameLogic.Views.Behaviours;
using UnityEngine;

namespace Tanks.GameLogic.Systems.AI
{
    internal sealed class AgentControlSystem : IExecuteSystem
    {
        private readonly AIContext _context;
        private readonly IGroup<AIEntity> _entities;
        private readonly int _allLayers = ~0;
        private List<AIEntity> _buffer = new List<AIEntity>();

        public AgentControlSystem(Contexts contexts)
        {
            _context = contexts.aI;
            _entities = contexts.aI.GetGroup(AIMatcher
                .AllOf(AIMatcher.NavMesh, AIMatcher.CanBeActive, AIMatcher.GameEntity, AIMatcher.Target,
                    AIMatcher.AgentDestination)
                .NoneOf(AIMatcher.Disabled, AIMatcher.ReadyToShoot));
        }

        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
                if (entity.navMesh.Value.remainingDistance <= _context.maxBallisticDistance.Value)
                {
                    Vector3 tankPosition = entity.gameEntity.Value.transform.Value.position + Vector3.up;
                    Vector3 targetPosition = entity.target.Value.position + Vector3.up;
                    Ray ray = new Ray(tankPosition, targetPosition - tankPosition);
                    if (Physics.SphereCast(ray, 0.5f, out RaycastHit hit, _context.maxBallisticDistance.Value,
                            _allLayers, QueryTriggerInteraction.Ignore) && hit.transform == entity.target.Value)
                    {
                        entity.isReadyToShoot = true;
                    }
                }
            }
        }
    }
}