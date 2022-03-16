using System.Threading.Tasks;
using Tanks.Data;
using UnityEngine;

namespace Tanks.General.Controllers
{
    public interface IUIController : IController
    {
        void Initialize(UIData data);
        Task ShowTeamMove(TeamType team, float moveTime);
        void ShowWinner(TeamType team);
        void SetPlayer(Transform target, string id);
        void RemoveMapElement(string id);
    }
}