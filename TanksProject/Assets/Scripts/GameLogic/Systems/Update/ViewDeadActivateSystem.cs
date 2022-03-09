using System.Collections.Generic;
using Entitas;
using Tanks.Data;
using Tanks.General.Services;
using UnityEngine;

namespace Tanks.GameLogic.Systems.Update
{
    internal sealed class ViewDeadActivateSystem : IExecuteSystem
    {
        private readonly SceneStaticData _staticData;
        private readonly IPoolService _poolService;
        private readonly IGroup<GameEntity> _entities;
        private readonly GameContext _context;
        private List<GameEntity> _buffer = new();

        public ViewDeadActivateSystem(Contexts contexts, SceneStaticData staticData, IPoolService poolService)
        {
            _staticData = staticData;
            _poolService = poolService;
            _entities = contexts.game.GetGroup(GameMatcher
                .AllOf(GameMatcher.View, GameMatcher.Dead)
                .NoneOf(GameMatcher.Destroy));
            _context = contexts.game;
        }

        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
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