using Entitas;
using UnityEngine;

namespace Tanks.GameLogic.Components.Game.Weapon
{
    [Game]
    public sealed class WeaponTransformComponent : IComponent
    {
        public Transform Value;
    }
}