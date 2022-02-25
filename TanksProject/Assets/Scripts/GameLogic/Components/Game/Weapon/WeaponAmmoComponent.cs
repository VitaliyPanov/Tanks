using Entitas;
using TanksGB.Data;

namespace TanksGB.GameLogic.Components.Game.Weapon
{
    [Game]
    public sealed class WeaponAmmoComponent : IComponent
    {
        public AmmoData Data;
    }
}