using Tanks.GameLogic.Systems.Weapon;

namespace Tanks.GameLogic.Systems.Update
{
    internal sealed class WeaponSystems : Feature
    {
        public WeaponSystems(Contexts contexts)
        {
            Add(new WeaponShootSystem(contexts));
            Add(new WeaponLaunchLoopSystem(contexts));
        }
    }
}