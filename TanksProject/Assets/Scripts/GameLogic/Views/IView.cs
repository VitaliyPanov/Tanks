using UnityEngine;

namespace Tanks.GameLogic.Views
{
    public interface IView
    {
        string UniqID { get; }
        Transform Transform { get; }
        GameObject GameObject { get; }
        void InitializeView(GameEntity entity);
        void DestroyView();
    }
}