using System.Collections.Generic;
using Entitas;
using Tanks.GameLogic.Services;

namespace Tanks.GameLogic.Systems.Input
{
    internal sealed class InputChangeControllableSystem : ReactiveSystem<InputEntity>
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly GameContext _context;
        private readonly List<GameEntity> _buffer = new();

        public InputChangeControllableSystem(GameContext gameContextContext, InputContext inputContext) : base(inputContext)
        {
            _entities = gameContextContext.GetGroup(GameMatcher.Movable);
            _context = gameContextContext;
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) =>
            context.CreateCollector(InputMatcher.ToggleNext.Added(), InputMatcher.TogglePrevious.Added());

        protected override bool Filter(InputEntity entity) => true;

        protected override void Execute(List<InputEntity> entities)
        {
            if (_entities.count > 1)
            {
                GameEntity nextSelected = entities[0].isToggleNext
                    ? _entities.GetEntities(_buffer).GetNext(_context.controllable.Entity)
                    : _entities.GetEntities(_buffer).GetPrevious(_context.controllable.Entity);
                nextSelected.tryControl = true;
            }
        }
    }
}