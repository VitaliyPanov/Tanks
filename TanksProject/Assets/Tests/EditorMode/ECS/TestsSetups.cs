using System.Collections.Generic;
using Tanks.Data;
using UnityEngine;

namespace Tanks.Tests.EditorMode.ECS
{
    public static class TestsSetups
    {
        internal static RuntimeData InstantiateRuntimeData(SceneStaticData staticData, AmmoData shellData)
        {
            RuntimeData runtimeData = ScriptableObject.CreateInstance<RuntimeData>();
            runtimeData.MovementSpeed = 1f;
            runtimeData.TurnSpeed = 1f;
            runtimeData.MoveTime = 1f;
            runtimeData.CurrentTeamMove = staticData.FirstMoveTeam;
            runtimeData.Shell = shellData;
            return runtimeData;
        }

        internal static SceneStaticData InstantiateStaticData()
        {
            SceneStaticData staticData = ScriptableObject.CreateInstance<SceneStaticData>();
            staticData.ExplosionPrefab = new GameObject();
            staticData.TankPrefab = new GameObject();
            staticData.TankSpawners = new List<TankSpawnerData>();
            staticData.PlayableTeam = TeamType.Blue;
            staticData.FirstMoveTeam = TeamType.Blue;
            staticData.StartTankHealth = 1f;
            return staticData;
        }

        internal static AmmoData InstantiateShellData()
        {
            var shellData = ScriptableObject.CreateInstance<AmmoData>();
            shellData.Prefab = new GameObject();
            shellData.Steam = new GameObject();
            shellData.Damage = 1f;
            shellData.Type = AmmoType.Shell;
            shellData.CooldownTime = 1f;
            shellData.ExplosionForce = 1f;
            shellData.ExplosionRadius = 1f;
            shellData.MaxLaunchForce = 1f;
            shellData.MaxLaunchForce = 1f;
            shellData.MaxLaunchingTime = 1f;
            return shellData;
        }
    }
}