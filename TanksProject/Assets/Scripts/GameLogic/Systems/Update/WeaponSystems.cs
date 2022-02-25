using TanksGB.GameLogic.Systems.Weapon;

namespace TanksGB.GameLogic.Systems.Update
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