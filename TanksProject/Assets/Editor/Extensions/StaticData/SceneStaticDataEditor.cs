using System.Linq;
using General.LevelDesign;
using Sirenix.OdinInspector.Editor;
using Tanks.Data;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TanksGB.Editor.Extensions.StaticData
{
    [CustomEditor(typeof(SceneStaticData))]
    public class SceneStaticDataEditor : OdinEditor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            SceneStaticData sceneData = (SceneStaticData) target;
            if (GUILayout.Button("Initialize"))
                InitializeSceneData(sceneData);
        }

        private void InitializeSceneData(SceneStaticData sceneData)
        {
            string currentScene = SceneManager.GetActiveScene().name;
            if (!sceneData.name.Contains(currentScene))
            {
                Debug.LogError($"The SceneData is not for {currentScene}");
                return;
            }
            sceneData.LevelKey = currentScene;
            sceneData.TankSpawners = FindObjectsOfType<TankSpawnMarker>()
                .Select(x => new TankSpawnerData(x.Type, x.transform.position))
                .ToList();
            sceneData.TankLayerMask = 1 << sceneData.TankPrefab.layer;
            EditorUtility.SetDirty(target);
            Debug.Log($"SceneData of {currentScene} initialized");
        }
    }
}