using System;

namespace General.Controllers
{
    public interface IUIController : IController
    {
        void ShowMessage(string team, float moveTime);
        event Action OnMessageHideEvent;
    }
}