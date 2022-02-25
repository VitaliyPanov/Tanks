using System;
using UnityEngine;

namespace Tanks.Data
{
    [CreateAssetMenu(fileName = "RuntimeData", menuName = "Tanks/RuntimeData")]
    public sealed class RuntimeData : ScriptableObject
    {
        public event Action<Transform> OnControllableTransformChangedEvent;
        public event Action OnTeamMoveChangedEvent;
        public float MoveTime;
        public float MovementSpeed;
        public float TurnSpeed;
        
        public AmmoData Shell;
        public TeamType CurrentTeamMove { get; private set; }
        public Transform Controllable { get; private set; }

        public void ChangeTeam(TeamType teamType)
        {
            CurrentTeamMove = teamType;
            OnTeamMoveChangedEvent?.Invoke();
        }
        
        public void ReplaceControllable(Transform target)
        {
            Controllable = target;
            OnControllableTransformChangedEvent?.Invoke(Controllable);
        }
    }
}