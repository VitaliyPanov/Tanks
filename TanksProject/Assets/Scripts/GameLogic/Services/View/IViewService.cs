using Entitas.CodeGeneration.Attributes;
using TanksGB.GameLogic.Views;
using UnityEngine;

namespace TanksGB.GameLogic.Services.View
{
    [Game, Unique, ComponentName("ViewService")]
    public interface IViewService
    {
        IView CreateView(GameObject prefab, GameEntity entity);
        void DestroyView(GameEntity entity);
    }
}