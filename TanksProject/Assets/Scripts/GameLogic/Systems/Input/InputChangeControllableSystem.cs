using System.Collections.Generic;
using Entitas;
using Tanks.GameLogic.Services;

namespace Tanks.GameLogic.Systems.Input
{
    internal sealed class InputChangeControllableSystem : ReactiveSystem<InputEntity>
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly GameContext _game;
        private List<GameEntity> _buffer = new();

        public InputChangeControllableSystem(Contexts contexts) : base(contexts.input)
        {
            _entities = contexts.game.GetGroup(GameMatcher.Movable);
            _game = contexts.game;
        }

        protected override ICollector<InputEntity> GetTrigger(IContext<InputEntity> context) =>
            context.CreateCollector(InputMatcher.ToggleNext.Added(), InputMatcher.TogglePrevious.Added());

        protected override bool Filter(InputEntity entity) => true;

        protected override void Execute(List<InputEntity> entities)
        {
            if (_entities.count > 1)
            {
                GameEntity nextSelected = entities[0].isToggleNext
                    ? _entities.GetEntities(_buffer).GetNext(_game.controllable.Entity)
                    : _entities.GetEntities(_buffer).GetPrevious(_game.controllable.Entity);
                nextSelected.tryControl = true;
            }
        }
    }
}