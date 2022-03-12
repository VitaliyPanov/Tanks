using System;
using Tanks.General.UI.Models;

namespace Tanks.General.UI.ViewModels
{
    public interface IWeaponViewModel
    {
        IWeaponModel WeaponModel { get; }
        event Action<bool, float> OnWeaponShellActivateEvent;
        void ToggleShellAim(bool isActive);
    }
}