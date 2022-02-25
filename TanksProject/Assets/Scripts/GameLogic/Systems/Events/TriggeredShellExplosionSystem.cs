using System.Collections.Generic;
using Entitas;
using UnityEngine;

namespace TanksGB.GameLogic.Systems.Events
{
    internal sealed class TriggeredShellExplosionSystem : IExecuteSystem
    {
        private readonly GameContext _context;
        private readonly IGroup<GameEntity> _triggeredEntities;
        private readonly IGroup<GameEntity> _targetEntities;
        private List<GameEntity> _buffer = new();

        public TriggeredShellExplosionSystem(Contexts contexts)
        {
            _context = contexts.game;
            _triggeredEntities =
                contexts.game.GetGroup(
                    GameMatcher.AllOf(GameMatcher.Shell, GameMatcher.Position, GameMatcher.Triggered));
            _targetEntities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Health, GameMatcher.Rigidbody));
        }

        public void Execute()
        {
            foreach (var entity in _triggeredEntities)
            {
                Vector3 explosionPosition = entity.position.Value;
                float explosionRadius = entity.shell.ExplosionRadius;
                float explosionForce = entity.shell.ExplosionForce;
                float damage = entity.damage.Value;

                foreach (var targetEntity in _targetEntities.GetEntities(_buffer))
                {
                    Rigidbody targetBody = targetEntity.rigidbody.Value;
                    float distance = Vector3.Distance(explosionPosition, targetBody.position);
                    if (distance < explosionRadius)
                    {
                        var factor = 1 - distance / explosionRadius;
                        GetDamage(targetEntity,damage * factor);
                        targetBody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
                    }
                }
                _context.viewService.value.DestroyView(entity);
                entity.isDestroy = true;
            }
        }

        private void GetDamage(GameEntity target, float damage)
        {
            GameEntity damageEntity = _context.CreateEntity();
            damageEntity.AddHealthDamage(damage);
            damageEntity.AddTarget(target);
        }
    }
}