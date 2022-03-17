using Tanks.Data;
using UnityEngine;

namespace Tanks.GameLogic.Views
{
    public class TeamBehaviour: MonoBehaviour
    {
        public TeamType Team { get; private set; }
        public void Construct(TeamType team) => Team = team;
    }
}