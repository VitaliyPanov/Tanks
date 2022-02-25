using Entitas;
using TanksGB.GameLogic.Services;

namespace TanksGB.GameLogic.Systems.Weapon
{
    internal sealed class WeaponLaunchLoopSystem : IExecuteSystem
    {
        private readonly IGroup<GameEntity> _entities;
        private readonly Contexts _contexts;

        public WeaponLaunchLoopSystem(Contexts contexts)
        {
            _contexts = contexts;
            _entities = contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.WeaponAmmo));
        }

        public void Execute()
        {
            foreach (var entity in _entities)
            {
                switch (entity.isWeaponActivate && !entity.isWeaponFired)
                {
                    case true when (!entity.isWeaponLaunching && !entity.isWeaponCooldown):
                        _contexts.game.SetTimer(entity.weaponAmmo.Data.MaxLaunchingTime,
                            GameComponentsLookup.WeaponLaunching, entity);
                        entity.isWeaponLaunching = true;
                        entity.ReplaceWeaponLaunchTime(0);
                        break;
                    case true when entity.isWeaponLaunching:
                        entity.weaponLaunchTime.Value += _contexts.input.deltaTime.Value;
                        break;
                    case false when entity.isWeaponLaunching:
                        entity.isWeaponLaunching = false;
                        break;
                }
            }
        }
    }
}