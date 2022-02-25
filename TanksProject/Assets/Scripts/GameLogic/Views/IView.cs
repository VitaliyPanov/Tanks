using UnityEngine;

namespace Tanks.GameLogic.Views
{
    public interface IView
    {
        Transform Transform { get; }
        GameObject GameObject { get; }
        void InitializeView(GameEntity entity);
        void DestroyView();
    }
}