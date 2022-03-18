using Entitas;
using Tanks.Data;
using Tanks.General.Services;
using UnityEngine;
using UnityEngine.AI;

namespace Tanks.GameLogic.Systems.AI
{
    internal sealed class AgentSetupSystem : IInitializeSystem
    {
        private const float c_tankFireAngleY = 10f;
        private const float c_tankFireY = 1.7f;
        private const float c_tankFireX = 1.35f;
        private readonly float _gravity = Physics.gravity.y;

        private readonly IDataService _dataService;
        private readonly IGroup<AIEntity> _entities;
        private readonly AIContext _context;

        public AgentSetupSystem(AIContext aiContext, IDataService dataService)
        {
            _dataService = dataService;
            _context = aiContext;
            _entities = _context.GetGroup(AIMatcher.NavMesh);
        }

        public void Initialize()
        {
            SetBallisticDistances();
            foreach (var entity in _entities)
            {
                entity.gameEntity.Value.rigidbody.Value.isKinematic = true;
                entity.isDisabled = true;
                NavMeshAgent meshAgent = entity.navMesh.Value;
                meshAgent.isStopped = true;
                meshAgent.stoppingDistance = 4f;
                meshAgent.speed = _dataService.RuntimeData.MovementSpeed;
                meshAgent.angularSpeed = _dataService.RuntimeData.TurnSpeed;
                meshAgent.autoBraking = false;
                meshAgent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
                meshAgent.avoidancePriority = Random.Range(25, 50);
                meshAgent.radius = 3f;
            }
        }

        private void SetBallisticDistances()
        {
            float fireAngleYRad = c_tankFireAngleY * Mathf.PI / 180;
            AmmoData shellData = _dataService.AmmunitionData(AmmoType.Shell);
            float accelerationFactor = 2 * Mathf.Pow(Mathf.Cos(fireAngleYRad), 2);

            float maxDiscriminant = Mathf.Pow(
                Mathf.Tan(fireAngleYRad) * Mathf.Pow(shellData.MaxLaunchForce, 2) * accelerationFactor,
                2) - 4 * _gravity * c_tankFireY * Mathf.Pow(shellData.MaxLaunchForce, 2) * accelerationFactor;
            float minDiscriminant = Mathf.Pow(
                Mathf.Tan(fireAngleYRad) * Mathf.Pow(shellData.MinLaunchForce, 2) * accelerationFactor,
                2) - 4 * _gravity * c_tankFireY * Mathf.Pow(shellData.MinLaunchForce, 2) * accelerationFactor;

            float maxDistanceRaw =
                (-Mathf.Pow(shellData.MaxLaunchForce, 2) * accelerationFactor * Mathf.Tan(fireAngleYRad) -
                 Mathf.Sqrt(maxDiscriminant)) / (2 * _gravity);

            float minDistanceRaw =
                (-Mathf.Pow(shellData.MinLaunchForce, 2) * accelerationFactor * Mathf.Tan(fireAngleYRad) -
                 Mathf.Sqrt(minDiscriminant)) / (2 * _gravity);

            _context.SetMaxBallisticDistance(maxDistanceRaw + c_tankFireX);
            _context.SetMinBallisticDistance(minDistanceRaw + c_tankFireX);
        }
    }
}