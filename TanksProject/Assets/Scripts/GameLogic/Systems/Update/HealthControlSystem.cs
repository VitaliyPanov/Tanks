using System.Collections.Generic;
using Entitas;

namespace Tanks.GameLogic.Systems.Update
{
    public sealed class HealthControlSystem : ReactiveSystem<GameEntity>
    {
        public HealthControlSystem(GameContext gameContext) : base(gameContext) {}
        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.CurrentHealth);

        protected override bool Filter(GameEntity entity) => true;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.currentHealth.Value <= 0)
                    entity.isDead = true;
                else if (entity.currentHealth.Value > entity.maxHealth.Value)
                    entity.currentHealth.Value = entity.maxHealth.Value;
            }
        }
    }
}