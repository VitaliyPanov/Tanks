using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Tanks.Data
{
    [CreateAssetMenu(fileName = "SceneData", menuName = "Tanks/StaticData/Scene")]
    public sealed class SceneStaticData : ScriptableObject
    {
        public string LevelKey;
        public TeamType FirstMoveTeam;
        public List<TankSpawnerData> TankSpawners;
        [FoldoutGroup("Prefab"), PreviewField]
        public GameObject TankPrefab;
        [FoldoutGroup("Prefab")]
        public GameObject ExplosionPrefab;

        public float StartTankHealth;
        public TeamType PlayableTeam;
    }
}
