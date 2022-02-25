using Entitas.CodeGeneration.Attributes;
using Tanks.GameLogic.Views;
using UnityEngine;

namespace Tanks.GameLogic.Services.View
{
    [Game, Unique, ComponentName("ViewService")]
    public interface IViewService
    {
        IView CreateView(GameObject prefab, GameEntity entity);
        void DestroyView(GameEntity entity);
    }
}