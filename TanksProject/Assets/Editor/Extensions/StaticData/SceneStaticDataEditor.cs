using System.Linq;
using Sirenix.OdinInspector.Editor;
using Tanks.Data;
using Tanks.General.LevelDesign;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tanks.Editor.Extensions.StaticData
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
            EditorUtility.SetDirty(target);
            Debug.Log($"SceneData of {currentScene} initialized");
        }
    }
}