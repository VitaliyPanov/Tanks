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
        private readonly RuntimeData _runtimeData;
        private readonly IGroup<AIEntity> _entities;
        private List<AIEntity> _buffer = new List<AIEntity>();
        private readonly AIContext _ai;
        private readonly AmmoData _shellData;
        private readonly InputContext _input;

        public AgentShootSystem(Contexts contexts, IDataService dataService)
        {
            _ai = contexts.aI;
            _input = contexts.input;
            _runtimeData = dataService.RuntimeData;
            _shellData = dataService.AmmunitionData(AmmoType.Shell);
            _entities = contexts.aI.GetGroup(AIMatcher
                .AllOf(AIMatcher.NavMesh, AIMatcher.CanBeActive, AIMatcher.GameEntity, AIMatcher.AgentDestination,
                    AIMatcher.Target, AIMatcher.ReadyToShoot)
                .NoneOf(AIMatcher.AgentShot, AIMatcher.Disabled));
        }

        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
                entity.navMesh.Value.enabled = false;
                Transform agentTransform = entity.gameEntity.Value.transform.Value;
                Vector3 targetDirection = entity.target.Value.position - agentTransform.position;
                agentTransform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(agentTransform.forward,
                    targetDirection, _input.fixedDeltaTime.Value, 0.0f));
                if (Vector3.Angle(agentTransform.forward, targetDirection) <= 1)
                {
                    float deltaDistance = Vector3.Distance(agentTransform.position, entity.target.Value.position) -
                                          _ai.minBallisticDistance.Value;
                    float launchingTime = 0 + (_shellData.MaxLaunchingTime / (_ai.maxBallisticDistance.Value -
                                                                              _ai.minBallisticDistance.Value)) *
                        deltaDistance;
                    launchingTime = Mathf.Clamp(launchingTime, 0f, _shellData.MaxLaunchingTime);
                    LaunchWeapon(entity.gameEntity.Value, launchingTime);
                    entity.isAgentShot = true;
                }
            }
        }

        private async void LaunchWeapon(GameEntity entity, float time)
        {
            Debug.Log(time);
            entity.ReplaceWeaponLaunchTime(time);
            entity.isWeaponLaunching = true;
            await Task.Delay((int) (1000 * time));
            entity.isWeaponActivate = true;
            entity.isWeaponLaunching = false;
        }
    }
}