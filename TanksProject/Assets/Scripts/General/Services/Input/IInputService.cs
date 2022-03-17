using System;
using UnityEngine;

namespace Tanks.General.Services.Input
{
    public interface IInputService
    {
        void RegisterTankListener(InputControls.ITankActions system);
        void RegisterUIListener(InputControls.IUIActions system);
    }
}