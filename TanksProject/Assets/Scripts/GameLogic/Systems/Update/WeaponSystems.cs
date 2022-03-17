using Tanks.GameLogic.Systems.Weapon;
using Tanks.General.Services;

namespace Tanks.GameLogic.Systems.Update
{
    internal sealed class WeaponSystems : Feature
    {
        public WeaponSystems(GameContext gameContext, InputContext inputContext, IPoolService poolService)
        {
            Add(new WeaponShootSystem(gameContext, poolService));
            Add(new WeaponLaunchLoopSystem(gameContext, inputContext));
        }
    }
}