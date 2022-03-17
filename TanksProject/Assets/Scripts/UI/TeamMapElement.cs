using Tanks.Data;
using UnityEngine;
using UnityEngine.UIElements;

namespace Tanks.UI
{
    public sealed class TeamMapElement : MapElement
    {
        private const string c_teamStyle = "element-team";

        public TeamMapElement(TeamType team)
        {
            var teamIcon = new Image();
            teamIcon.AddToClassList(c_teamStyle);
            teamIcon.style.unityBackgroundImageTintColor = TeamColors.TeamColor(team);
            Insert(0, teamIcon);
        }
    }
}