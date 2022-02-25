using System;
using General;
using General.LevelDesign;
using Tanks.Data;
using UnityEditor;
using UnityEngine;

namespace TanksGB.Editor.Extensions
{
    [UnityEditor.CustomEditor(typeof(TankSpawnMarker))]
    public sealed class SpawnMarkerEditor:UnityEditor.Editor
    {
        [DrawGizmo(GizmoType.Active | GizmoType.Pickable | GizmoType.NonSelected)]
        public static void RenderCustomGizmo(TankSpawnMarker tankSpawnMarker, GizmoType gizmo)
        {
            Gizmos.color = TeamColors.TeamColor(tankSpawnMarker.Type);
            Gizmos.DrawSphere(tankSpawnMarker.transform.position, 0.5f);
        }
    }
}