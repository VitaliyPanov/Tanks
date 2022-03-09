using Tanks.GameLogic.Systems.Weapon;
using Tanks.General.Services;

namespace Tanks.GameLogic.Systems.Update
{
    internal sealed class WeaponSystems : Feature
    {
        public WeaponSystems(Contexts contexts, IPoolService poolService)
        {
            Add(new WeaponShootSystem(contexts, poolService));
            Add(new WeaponLaunchLoopSystem(contexts));
        }
    }
}