using Entitas;

namespace Tanks.GameLogic.Services
{
    public interface IEventListener
    {
        void AddListener(IEntity entity);
    }
}