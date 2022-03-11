using Tanks.Data;
using Tanks.General.LevelDesign;
using UnityEditor;
using UnityEngine;

namespace Tanks.Editor.Extensions
{
    [CustomEditor(typeof(TankSpawnMarker))]
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