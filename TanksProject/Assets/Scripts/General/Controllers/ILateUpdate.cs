namespace Tanks.General.Controllers
{
    public interface ILateUpdate : IController
    {
        void LateUpdate();
    }
}