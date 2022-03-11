using System.Collections.Generic;
using Tanks.GameLogic.Services;
using Tanks.General.Services;
using UnityEngine;

namespace Tanks.Core.Infrastructure.Services.Pool
{
    public sealed class PoolService : IPoolService
    {
        private readonly Dictionary<string, ObjectPool> _viewCache = new(16);
        
        public T Instantiate<T>(GameObject prefab, Transform parent = null) where T : Component
        {
            if (!_viewCache.TryGetValue(prefab.name, out ObjectPool viewPool))
            {
                viewPool = new ObjectPool(prefab);
                _viewCache[prefab.name] = viewPool;
            }

            return viewPool.Pop(parent).GetOrAddComponent<T>();
        }
        
        public void Destroy(GameObject gameObject)
        {
            _viewCache[gameObject.name].Push(gameObject); 
        }
    }
}