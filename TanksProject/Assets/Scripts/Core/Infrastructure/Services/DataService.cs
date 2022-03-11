using System.Collections.Generic;
using System.Linq;
using Tanks.Data;
using Tanks.General.Services;
using UnityEngine;

namespace Tanks.Core.Infrastructure.Services
{
    public class DataService : IDataService
    {
        private Dictionary<string, SceneStaticData> _scenes;
        private Dictionary<AmmoType, AmmoData> _ammunition;
        public RuntimeData RuntimeData { get; private set; }

        public AmmoData AmmunitionData(AmmoType type) =>
            _ammunition.TryGetValue(type, out AmmoData ammoData) ? ammoData : null;

        public UIData UIData { get; private set; }

        public SceneStaticData StaticData(string sceneName) =>
            _scenes.TryGetValue(sceneName, out SceneStaticData staticData) ? staticData : null;

        public void Load()
        {
            _scenes = Resources.LoadAll<SceneStaticData>(DataPaths.SCENE)
                .ToDictionary(x => x.LevelKey, x => x);
            _ammunition = Resources.LoadAll<AmmoData>(DataPaths.AMMO).ToDictionary(x => x.Type, x => x); 
            RuntimeData = Resources.Load<RuntimeData>(DataPaths.RUNTIME + "/RuntimeData");
            UIData = Resources.Load<UIData>(DataPaths.UI + "/UIData");
        }
    }
}