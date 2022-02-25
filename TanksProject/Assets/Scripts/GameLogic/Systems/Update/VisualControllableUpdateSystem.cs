using System.Collections.Generic;
using Entitas;
using Tanks.Data;
using UnityEngine;

namespace Tanks.GameLogic.Systems.Update
{
    internal sealed class VisualControllableUpdateSystem : ReactiveSystem<GameEntity>, ICleanupSystem
    {
        private static readonly string s_emissioncolor = "_EmissionColor";
        private readonly RuntimeData _runtimeData;
        private readonly GameContext _game;
        private readonly IGroup<GameEntity> _selectGroup;
        private List<GameEntity> _buffer = new();

        public VisualControllableUpdateSystem(Contexts contexts, RuntimeData runtimeData) : base(contexts.game)
        {
            _game = contexts.game;
            _selectGroup = contexts.game.GetGroup(GameMatcher.Control);
            _runtimeData = runtimeData;
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.Control.AddedOrRemoved());

        protected override bool Filter(GameEntity entity) => entity.hasTransform;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                if (entity.tryControl)
                {
                    _game.ReplaceControllable(entity);
                    _runtimeData.ReplaceControllable(entity.transform.Value);
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
            GameEntity controllableEntity = _game.controllableEntity.controllable.Entity;
            foreach (var entity in _selectGroup.GetEntities(_buffer))
            {
                if (controllableEntity != entity)
                    entity.tryControl = false;
            }
        }
    }
}