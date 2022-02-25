using UnityEngine;

namespace General.Services
{
    public interface IPoolService
    {
        T Instantiate<T>(GameObject prefab) where T : Component;
        void Destroy(GameObject gameObject);
    }
}