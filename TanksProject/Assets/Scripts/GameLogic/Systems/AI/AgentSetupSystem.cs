﻿using Entitas;
using Tanks.Data;
using Tanks.General.Services;
using UnityEngine;
using UnityEngine.AI;

namespace Tanks.GameLogic.Systems.AI
{
    internal sealed class AgentSetupSystem : IInitializeSystem
    {
        private readonly IDataService _dataService;
        private readonly IGroup<AIEntity> _entities;
        private readonly AIContext _context;

        private float _tankFireAngleY = 10f;
        private float _tankFireY = 1.7f;
        private float _tankFireX = 1.35f;
        private float _gravity = Physics.gravity.y;

        public AgentSetupSystem(Contexts contexts, IDataService dataService)
        {
            _dataService = dataService;
            
            _context = contexts.aI;
            _entities = contexts.aI.GetGroup(AIMatcher.NavMesh);
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
            float fireAngleYRad = _tankFireAngleY * Mathf.PI / 180;
            AmmoData shellData = _dataService.AmmunitionData(AmmoType.Shell);
            float accelerationFactor = 2 * Mathf.Pow(Mathf.Cos(fireAngleYRad), 2);

            float maxDiscriminant = Mathf.Pow(
                Mathf.Tan(fireAngleYRad) * Mathf.Pow(shellData.MaxLaunchForce, 2) * accelerationFactor,
                2) - 4 * _gravity * _tankFireY * Mathf.Pow(shellData.MaxLaunchForce, 2) * accelerationFactor;
            float minDiscriminant = Mathf.Pow(
                Mathf.Tan(fireAngleYRad) * Mathf.Pow(shellData.MinLaunchForce, 2) * accelerationFactor,
                2) - 4 * _gravity * _tankFireY * Mathf.Pow(shellData.MinLaunchForce, 2) * accelerationFactor;

            float maxDistanceRaw =
                (-Mathf.Pow(shellData.MaxLaunchForce, 2) * accelerationFactor * Mathf.Tan(fireAngleYRad) -
                 Mathf.Sqrt(maxDiscriminant)) / (2 * _gravity);

            float minDistanceRaw =
                (-Mathf.Pow(shellData.MinLaunchForce, 2) * accelerationFactor * Mathf.Tan(fireAngleYRad) -
                 Mathf.Sqrt(minDiscriminant)) / (2 * _gravity);

            _context.SetMaxBallisticDistance(maxDistanceRaw + _tankFireX);
            _context.SetMinBallisticDistance(minDistanceRaw + _tankFireX);
        }
    }
}