using System.Collections.Generic;
using Entitas;
using Tanks.Data;
using Tanks.GameLogic.Services;

namespace Tanks.GameLogic.Systems.Weapon
{
    internal sealed class WeaponLaunchLoopSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly Contexts _contexts;
        private List<GameEntity> _buffer = new List<GameEntity>();

        public WeaponLaunchLoopSystem(Contexts contexts)
        {
            _contexts = contexts;
            _entities = contexts.game.GetGroup(GameMatcher
                .AllOf(GameMatcher.WeaponActivate, GameMatcher.WeaponTransform)
                .NoneOf(GameMatcher.WeaponFired, GameMatcher.WeaponCooldown));
        }

        public void Execute()
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
                if (!entity.isWeaponLaunching)
                {
                    entity.isWeaponLaunching = true;
                    _contexts.game.SetTimer(entity.weaponAmmo.Data.MaxLaunchingTime, GameComponentsLookup.WeaponLaunching, entity);
                    entity.ReplaceWeaponLaunchTime(0);
                }
                else
                {
                    entity.weaponLaunchTime.Value += _contexts.input.deltaTime.Value;
                }
            }
        }
    }
}