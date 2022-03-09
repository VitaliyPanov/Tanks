using System.Collections.Generic;
using Entitas;
using Tanks.Data;
using Tanks.General.Services;
using UnityEngine;

namespace Tanks.GameLogic.Systems.Events
{
    internal sealed class TriggeredShellExplosionSystem : IExecuteSystem
    {
        private readonly IPoolService _poolService;
        private readonly GameContext _context;
        private readonly IGroup<GameEntity> _triggeredEntities;
        private readonly IGroup<GameEntity> _targetEntities;
        private List<GameEntity> _buffer = new();
        private readonly AmmoData _shellData;

        public TriggeredShellExplosionSystem(Contexts contexts, RuntimeData runtimeData, IPoolService poolService)
        {
            _shellData = runtimeData.Shell;
            _poolService = poolService;
            _context = contexts.game;
            _triggeredEntities = contexts.game.GetGroup(GameMatcher
                .AllOf(GameMatcher.Shell, GameMatcher.ShellSteam, GameMatcher.Position, GameMatcher.Triggered));
            
            _targetEntities = contexts.game.GetGroup(GameMatcher
                .AllOf(GameMatcher.CurrentHealth, GameMatcher.Rigidbody)
                .NoneOf(GameMatcher.Dead));
        }

        public void Execute()
        {
            foreach (var entity in _triggeredEntities)
            {
                Vector3 explosionPosition = entity.position.Value;
                float explosionRadius = entity.shell.ExplosionRadius;
                float explosionForce = entity.shell.ExplosionForce;
                float damage = entity.damage.Value;
                entity.shellSteam.Object.parent = null;
                InstantiateExplosion(explosionPosition);

                foreach (var targetEntity in _targetEntities.GetEntities(_buffer))
                {
                    Rigidbody targetBody = targetEntity.rigidbody.Value;
                    float distance = Vector3.Distance(explosionPosition, targetBody.position);
                    if (distance < explosionRadius)
                    {
                        var factor = 1 - distance / explosionRadius;
                        targetEntity.ReplaceHealthDamage(damage * factor);
                        targetBody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
                    }
                }

                _context.viewService.value.DestroyView(entity);
                entity.isDestroy = true;
            }
        }

        private void InstantiateExplosion(Vector3 explosionPosition)
        {
            ParticleSystem explosion = _poolService.Instantiate<ParticleSystem>(_shellData.ShellExplosion);
            explosion.transform.position = explosionPosition;
            explosion.Play();
            _context.CreateEntity().AddParticle(explosion);
        }
    }
}