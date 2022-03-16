using System.Collections.Generic;
using Entitas;
using Tanks.Data;
using Tanks.General.Controllers;
using Tanks.General.Services;
using UnityEngine;

namespace Tanks.GameLogic.Systems.Update
{
    public sealed class ViewDeadActivateSystem : IExecuteSystem
    {
        private readonly SceneStaticData _staticData;
        private readonly IPoolService _poolService;
        private readonly IControllersMediator _mediator;
        private readonly IGroup<GameEntity> _entities;
        private readonly GameContext _context;
        private List<GameEntity> _buffer = new();

        public ViewDeadActivateSystem(Contexts contexts, SceneStaticData staticData, IPoolService poolService, IControllersMediator mediator)
        {
            _staticData = staticData;
            _poolService = poolService;
            _mediator = mediator;
            _entities = contexts.game.GetGroup(GameMatcher
                .AllOf(GameMatcher.View, GameMatcher.Transform, GameMatcher.Dead)
                .NoneOf(GameMatcher.Destroy));
            _context = contexts.game;
        }

        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
                _mediator.OnDestroyView(entity.transform.Value, entity.view.Value.UniqID);
                InstantiateExplosion(entity.view.Value.Transform.position);
                entity.isDestroy = true;
            }
        }

        private void InstantiateExplosion(Vector3 position)
        {
            ParticleSystem explosion = _poolService.Instantiate<ParticleSystem>(_staticData.ExplosionPrefab);
            explosion.transform.position = position;
            explosion.Play();
            _context.CreateEntity().AddParticle(explosion);
        }
    }
}