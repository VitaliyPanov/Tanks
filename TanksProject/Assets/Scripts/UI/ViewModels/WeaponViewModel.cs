using System;
using Tanks.General.UI.Models;
using Tanks.General.UI.ViewModels;

namespace Tanks.UI.ViewModels
{
    public sealed class WeaponViewModel : IWeaponViewModel
    {
        public IWeaponModel WeaponModel { get; }
        public event Action<bool, float> OnWeaponShellActivateEvent;
        public WeaponViewModel(IWeaponModel weaponModel) => WeaponModel = weaponModel;
        public void ToggleShellAim(bool isActive) => OnWeaponShellActivateEvent?.Invoke(isActive, WeaponModel.LaunchingTime);
    }
}