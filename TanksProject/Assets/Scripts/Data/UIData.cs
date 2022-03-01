using UnityEngine;

namespace Tanks.Data
{
    [CreateAssetMenu(fileName = "UIData", menuName = "Tanks/UIData")]
    public sealed class UIData : ScriptableObject
    {
        public GameObject InterfacePrefab;
        public GameObject HealthAimCanvas;
        public GameObject AimCanvas;
    }
}