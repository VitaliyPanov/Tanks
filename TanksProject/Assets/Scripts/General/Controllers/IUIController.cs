using System;
using UnityEngine;

namespace General.Controllers
{
    public interface IUIController : IController
    {
        void Initialize(GameObject prefab);
        void ShowMessage(string team, float moveTime);
        event Action OnMessageHideEvent;
    }
}