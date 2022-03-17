using System.Collections.Generic;
using Entitas;
using Tanks.General.Controllers;
using UnityEngine;

namespace Tanks.GameLogic.Systems.Update
{
    internal sealed class ControllableUpdateSystem : ReactiveSystem<GameEntity>, ICleanupSystem
    {
        private static readonly string s_emissioncolor = "_EmissionColor";
        private readonly IControllersMediator _mediator;
        private readonly GameContext _context;
        private readonly IGroup<GameEntity> _selectGroup;
        private List<GameEntity> _buffer = new();

        public ControllableUpdateSystem(GameContext gameContext, IControllersMediator mediator) : base(gameContext)
        {
            _context = gameContext;
            _selectGroup = gameContext.GetGroup(GameMatcher.Control);
            _mediator = mediator;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.Control.AddedOrRemoved());

        protected override bool Filter(GameEntity entity) => entity.hasView && entity.hasTransform;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.tryControl)
                {
                    _context.ReplaceControllable(entity);
                    _mediator.ReplaceControllable(entity.transform.Value, entity.view.Value.UniqID);
                    SetEmission(entity, Color.white * 0.25f);
                }
                else
                    SetEmission(entity, Color.black);
            }
        }

        private static void SetEmission(GameEntity entity, Color color)
        {
            foreach (var renderer in entity.meshRenderer.Array)
            {
                renderer.material.SetColor(s_emissioncolor, color);
            }
        }
        public void Cleanup()
        { 
            GameEntity controllableEntity = _context.controllableEntity.controllable.Entity;
            foreach (var entity in _selectGroup.GetEntities(_buffer))
            {
                if (controllableEntity != entity)
                    entity.tryControl = false;
            }
        }
    }
}