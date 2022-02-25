using General.Services;
using Tanks.GameLogic.Views;
using UnityEngine;

namespace Tanks.GameLogic.Services.View
{
    internal sealed class ViewService : IViewService
    {
        private readonly IPoolService _poolService;
        public ViewService(IPoolService poolService)
        {
            _poolService = poolService;
        }

        public IView CreateView(GameObject prefab, GameEntity entity)
        {
            var view = _poolService.Instantiate<UnityView>(prefab);
            view.InitializeView(entity);
            return view;
        }

        public void DestroyView(GameEntity entity)
        {
            if (entity.hasView)
            {
                entity.view.Value.DestroyView();
                _poolService.Destroy(entity.view.Value.GameObject);
                entity.RemoveView();
            }
        }
    }
}