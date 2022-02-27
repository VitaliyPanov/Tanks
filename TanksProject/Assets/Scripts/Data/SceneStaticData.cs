using System.Collections.Generic;
using UnityEngine;

namespace Tanks.Data
{
    [CreateAssetMenu(fileName = "SceneData", menuName = "Tanks/StaticData/Scene")]
    public sealed class SceneStaticData : ScriptableObject
    {
        public string LevelKey;
        public TeamType FirstMoveTeam;
        public List<TankSpawnerData> TankSpawners;
        //[FoldoutGroup("Prefab"), PreviewField]
        public GameObject TankPrefab;

        public float StartTankHealth;
        public TeamType PlayableTeam;
    }
}
