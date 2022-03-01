using System.Threading.Tasks;
using Tanks.Data;
using UnityEngine;

namespace Tanks.General.Controllers
{
    public interface IUIController : IController
    {
        void Initialize(UIData data);
        Task ShowMessage(string team, float moveTime);
    }
}