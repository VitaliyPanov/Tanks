using UnityEngine;

namespace General.Controllers
{
    public interface ICameraController : IController
    {
        void SetCamera(Camera camera);
        void SetTarget(Transform target);
    }
}