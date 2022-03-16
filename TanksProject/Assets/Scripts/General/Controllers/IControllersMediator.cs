using Tanks.Data;
using UnityEngine;

namespace Tanks.General.Controllers
{
    public interface IControllersMediator
    {
        void ReplaceControllable(Transform target, string id);
        void OnDestroyView(Transform target, string id);
        void ChangeTeam(TeamType team);
        void SetWinner(TeamType team);
    }
}