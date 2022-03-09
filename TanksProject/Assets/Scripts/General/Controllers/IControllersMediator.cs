using Tanks.Data;
using UnityEngine;

namespace Tanks.General.Controllers
{
    public interface IControllersMediator
    {
        void ReplaceControllable(Transform transformValue);
        void ChangeTeam(TeamType team);
        void SetWinner(TeamType team);
    }
}