using Tanks.General.UI.Models;

namespace Tanks.UI.Models
{
    public sealed class WeaponModel: IWeaponModel
    {
        public float LaunchingTime { get; }
        public WeaponModel(float maxLaunchingTime) => LaunchingTime = maxLaunchingTime;
    }
}