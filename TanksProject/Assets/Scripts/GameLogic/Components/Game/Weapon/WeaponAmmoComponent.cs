using Entitas;
using Tanks.Data;

namespace Tanks.GameLogic.Components.Game.Weapon
{
    [Game]
    public sealed class WeaponAmmoComponent : IComponent
    {
        public AmmoData Data;
    }
}