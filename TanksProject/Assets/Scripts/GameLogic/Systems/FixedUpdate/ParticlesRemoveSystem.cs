using System.Collections.Generic;
using Entitas;
using Tanks.General.Services;
using UnityEngine;

namespace Tanks.GameLogic.Systems.FixedUpdate
{
    internal sealed class ParticlesRemoveSystem : IExecuteSystem
    {
        private readonly IPoolService _poolService;
        private readonly GameContext _context;
        private readonly IGroup<GameEntity> _entities;
        private List<GameEntity> _buffer = new List<GameEntity>();

        public ParticlesRemoveSystem(Contexts contexts, IPoolService poolService)
        {
            _poolService = poolService;
            _context = contexts.game;
            _entities = contexts.game.GetGroup(GameMatcher.Particle);
        }

        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
                ParticleSystem system = entity.particle.Value;
                if (system.isStopped)
                {
                    system.Simulate(0);
                    system.Pause();
                    _poolService.Destroy(system.gameObject);
                    entity.isDestroy = true;
                }
            }
        }
    }
}