using Tanks.Data;

namespace Tanks.General.Controllers
{
    public interface ILogicController : IController
    {
        void Initialize(string sceneName);
        void Pause(bool isPause);
    }
}