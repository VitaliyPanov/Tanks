using Entitas;
using UnityEngine;

namespace TanksGB.GameLogic.Systems.Update
{
    public sealed class DamageImplementSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;

        public DamageImplementSystem(Contexts contexts) =>
            _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.HealthDamage, GameMatcher.Target));

        public void Execute()
        {
            foreach (var entity in _entities)
            {
                var target = entity.target.Value;
                if (target.hasCurrentHealth)
                {
                    target.ReplaceCurrentHealth(target.currentHealth.Value - entity.healthDamage.Value);
                }
                entity.isDestroy = true;
            }
        }
    }
}