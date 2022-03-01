using UnityEngine;

namespace Tanks.General.Controllers
{
    public interface ICameraController : IController
    {
        void Initialize(Camera camera);
        void SetTarget(Transform target);
    }
}