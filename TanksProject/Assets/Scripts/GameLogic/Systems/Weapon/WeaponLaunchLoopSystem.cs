using System.Collections.Generic;
using Entitas;
using Tanks.Data;
using Tanks.GameLogic.Services;

namespace Tanks.GameLogic.Systems.Weapon
{
    internal sealed class WeaponLaunchLoopSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly GameContext _gameContext;
        private readonly InputContext _inputContext;
        private List<GameEntity> _buffer = new List<GameEntity>();

        public WeaponLaunchLoopSystem(GameContext gameContext, InputContext inputContext)
        {
            _gameContext = gameContext;
            _inputContext = inputContext;
            _entities = gameContext.GetGroup(GameMatcher
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
                    _gameContext.SetTimer(entity, GameComponentsLookup.WeaponActivate, entity.weaponAmmo.Data.MaxLaunchingTime);
                    entity.ReplaceWeaponLaunchTime(0);
                }
                else
                {
                    entity.weaponLaunchTime.Value += _inputContext.deltaTime.Value;
                }
            }
        }
    }
}