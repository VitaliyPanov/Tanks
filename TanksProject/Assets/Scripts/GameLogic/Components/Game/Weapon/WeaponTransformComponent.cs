using Entitas;
using UnityEngine;

namespace TanksGB.GameLogic.Components.Game.Weapon
{
    [Game]
    public sealed class WeaponTransformComponent : IComponent
    {
        public Transform Value;
    }
}