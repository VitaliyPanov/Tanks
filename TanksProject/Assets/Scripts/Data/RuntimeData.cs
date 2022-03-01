using System;
using UnityEngine;

namespace Tanks.Data
{
    [CreateAssetMenu(fileName = "RuntimeData", menuName = "Tanks/RuntimeData")]
    public sealed class RuntimeData : ScriptableObject
    {
        public float MoveTime;
        public float MovementSpeed;
        public float TurnSpeed;
        
        public AmmoData Shell;
        public TeamType CurrentTeamMove;
        public Transform Controllable;
    }
}