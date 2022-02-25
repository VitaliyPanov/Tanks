using Entitas;

namespace Tanks.GameLogic.Components.Game.Ammunition
{
    [Game]
    public sealed class ShellComponent : IComponent
    {
        public float ExplosionForce;
        public float ExplosionRadius;
    }
}