using System.Collections.Generic;
using Entitas;
using Tanks.General.Services;

namespace Tanks.GameLogic.Systems.Update
{
    public sealed class TimeTrippingSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly InputContext _inputContext;
        private readonly List<GameEntity> _buffer = new();

        public TimeTrippingSystem(GameContext gameContext, InputContext inputContext)
        {
            _entities = gameContext.GetGroup(GameMatcher
                .AllOf(GameMatcher.Timer, GameMatcher.Target, GameMatcher.ComponentIndex)
                .NoneOf(GameMatcher.Destroy));
            _inputContext = inputContext;
        }
        
        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
                var timer = entity.timer;
                if (!entity.target.Value.HasComponent(entity.componentIndex.Value))
                    entity.isDestroy = true;
                else
                {
                    timer.Value -= _inputContext.fixedDeltaTime.Value;
                    if (timer.Value <= 0f)
                    {
                        entity.target.Value.RemoveComponent(entity.componentIndex.Value);
                        entity.isDestroy = true;
                    }
                }
            }
        }
    }
}