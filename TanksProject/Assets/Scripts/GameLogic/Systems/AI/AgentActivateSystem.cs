using System.Collections.Generic;
using Entitas;
using UnityEngine;
using UnityEngine.AI;

namespace Tanks.GameLogic.Systems.AI
{
    internal sealed class AgentActivateSystem : ReactiveSystem<AIEntity>
    {
        private const float c_maxDistance = float.MaxValue;
        private readonly IGroup<GameEntity> _tankEntities;

        public AgentActivateSystem(AIContext aiContext, GameContext gameContext) : base(aiContext)
        {
            _tankEntities = gameContext.GetGroup(GameMatcher
                .AllOf(GameMatcher.Health, GameMatcher.Transform)
                .NoneOf(GameMatcher.Movable, GameMatcher.Dead));
        }

        protected override ICollector<AIEntity> GetTrigger(IContext<AIEntity> context) =>
            context.CreateCollector(AIMatcher.Disabled.Removed());

        protected override bool Filter(AIEntity entity) => entity.hasGameEntity && entity.hasNavMesh;

        protected override void Execute(List<AIEntity> entities)
        {
            foreach (var entity in entities)
            {
                Vector3 agentPosition = entity.gameEntity.Value.transform.Value.position;
                float enemyDistance = c_maxDistance;
                Transform target = null;
                foreach (var tank in _tankEntities)
                {
                    Transform tankTransform = tank.transform.Value;
                    float distance = Vector3.Distance(agentPosition, tankTransform.position);
                    if (distance < enemyDistance && tank.team.Type != entity.gameEntity.Value.team.Type)
                    {
                        enemyDistance = distance;
                        target = tankTransform;
                    }
                }

                if (target != null)
                {
                    RefreshNavMesh(entity, target.position);
                    RefreshEntity(entity, target);
                }
                else
                    entity.gameEntity.Value.isMovable = false;
            }
        }

        private static void RefreshEntity(AIEntity entity, Transform target)
        {
            entity.ReplaceTarget(target);
            entity.ReplaceAgentDestination(target.position);
            entity.isReadyToShoot = false;
            entity.isAgentShot = false;
        }

        private void RefreshNavMesh(AIEntity entity, Vector3 destination)
        {
            NavMeshAgent navMeshAgent = entity.navMesh.Value;
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }
    }
}