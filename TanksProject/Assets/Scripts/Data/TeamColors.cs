using UnityEngine;

namespace Tanks.Data
{
    public static class TeamColors
    {
        public static Color TeamColor(TeamType type) => type switch
        {
            TeamType.Blue => Color.blue,
            TeamType.Red => Color.red,
            TeamType.Black => Color.black,
            _ => Color.gray
        };
    }
}