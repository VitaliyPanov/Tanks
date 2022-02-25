using System.Collections.Generic;
using System.Linq;
using General.Services;
using TanksGB.Data;
using UnityEngine;

namespace TanksGB.Core.Infrastructure.Services
{
    internal sealed class DataService : IDataService
    {
        private Dictionary<string, SceneStaticData> _scenes;
        public RuntimeData RuntimeData { get; private set; }

        public SceneStaticData StaticData(string sceneName) =>
            _scenes.TryGetValue(sceneName, out SceneStaticData staticData) ? staticData : null;

        public void Load()
        {
            _scenes = Resources.LoadAll<SceneStaticData>(DataPaths.SCENE)
                .ToDictionary(x => x.LevelKey, x => x);
            RuntimeData = Resources.Load<RuntimeData>(DataPaths.RUNTIME + "/RuntimeData");
        }
    }
}