using System.Collections.Generic;
using System.Threading.Tasks;
using Entitas;
using Tanks.Data;
using Tanks.General.Services;
using UnityEngine;

namespace Tanks.GameLogic.Systems.AI
{
    internal sealed class AgentShootSystem : IExecuteSystem
    {
        private readonly IGroup<AIEntity> _entities;
        private readonly List<AIEntity> _buffer = new();
        private readonly AIContext _aiContext;
        private readonly AmmoData _shellData;
        private readonly InputContext _inputContext;

        public AgentShootSystem(AIContext aiContext, InputContext inputContext, IDataService dataService)
        {
            _aiContext = aiContext;
            _inputContext = inputContext;
            _shellData = dataService.AmmunitionData(AmmoType.Shell);
            _entities = _aiContext.GetGroup(AIMatcher
                .AllOf(AIMatcher.NavMesh, AIMatcher.CanBeActive, AIMatcher.GameEntity, AIMatcher.AgentDestination,
                    AIMatcher.Target, AIMatcher.ReadyToShoot)
                .NoneOf(AIMatcher.AgentShot, AIMatcher.Disabled));
        }

        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
                entity.navMesh.Value.isStopped = true;
                
                Transform agentTransform = entity.gameEntity.Value.transform.Value;
                Vector3 targetDirection = entity.target.Value.position - agentTransform.position;
                agentTransform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(agentTransform.forward,
                    targetDirection, _inputContext.fixedDeltaTime.Value, 0.0f));
                if (Vector3.Angle(agentTransform.forward, targetDirection) <= 1)
                {
                    float deltaDistance = Vector3.Distance(agentTransform.position, entity.target.Value.position) -
                                          _aiContext.minBallisticDistance.Value;
                    float launchingTime = 0 + (_shellData.MaxLaunchingTime / (_aiContext.maxBallisticDistance.Value -
                                                                              _aiContext.minBallisticDistance.Value)) *
                        deltaDistance;
                    launchingTime = Mathf.Clamp(launchingTime, 0f, _shellData.MaxLaunchingTime);
                    LaunchWeapon(entity.gameEntity.Value, launchingTime);
                    entity.isAgentShot = true;
                }
            }
        }

        private async void LaunchWeapon(GameEntity entity, float time)
        {
            entity.ReplaceWeaponLaunchTime(time);
            entity.isWeaponLaunching = true;
            await Task.Delay((int) (1000 * time));
            entity.isWeaponActivate = true;
            entity.isWeaponLaunching = false;
        }
    }
}