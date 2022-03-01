using Tanks.Data;

namespace Tanks.General.Controllers
{
    public interface ILogicController : IController
    {
        void Initialize(SceneStaticData staticData, RuntimeData runtimeData);
        void Pause(bool isPause);
    }
}