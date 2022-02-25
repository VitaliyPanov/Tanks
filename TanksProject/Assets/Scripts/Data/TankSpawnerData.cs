using System;
using UnityEngine;

namespace Tanks.Data
{
    
    [Serializable]
    public class TankSpawnerData
    {
        public TeamType Type;
        public Vector3 Position;

        public TankSpawnerData(TeamType type, Vector3 position)
        {
            Type = type;
            Position = position;
        }
    }
    
}
