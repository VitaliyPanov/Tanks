using Sirenix.OdinInspector;
using UnityEngine;

namespace Tanks.Data
{
    [CreateAssetMenu(fileName = "AmmoName", menuName = "Tanks/StaticData/Ammo")]
    public sealed class AmmoData : ScriptableObject
    {
        public AmmoType Type;

        [FoldoutGroup(SOInspectorNames.PREFABS), PreviewField]
        public GameObject Prefab;

        [FoldoutGroup(SOInspectorNames.VARIABLES)]
        public float Damage;

        [FoldoutGroup(SOInspectorNames.VARIABLES)] [ShowIf(nameof(Type), AmmoType.Shell)]
        public float ExplosionForce;
        
        [FoldoutGroup(SOInspectorNames.VARIABLES)] [ShowIf(nameof(Type), AmmoType.Shell)]
        public float ExplosionRadius;
        
        [FoldoutGroup(SOInspectorNames.CONSTANTS)]
        public float MinLaunchForce;
        
        [FoldoutGroup(SOInspectorNames.CONSTANTS)]
        public float MaxLaunchForce;

        [FoldoutGroup(SOInspectorNames.CONSTANTS)]
        public float MaxLaunchingTime;

        [FoldoutGroup(SOInspectorNames.CONSTANTS)]
        public float CooldownTime;
    }

    public enum AmmoType
    {
        Shell,
        Bullet
    }
}