using System.Linq;
using Entitas;
using TanksGB.Data;
using TanksGB.GameLogic.Services;
using TanksGB.GameLogic.Views;
using UnityEngine;

namespace TanksGB.GameLogic.Systems.Init
{
    internal sealed class TanksInitSystem : IInitializeSystem
    {
        private const string c_fireTransform = "FireTransform";
        private readonly GameContext _context;
        private readonly SceneStaticData _staticData;
        private readonly RuntimeData _runtimeData;

        public TanksInitSystem(Contexts contexts, SceneStaticData staticData, RuntimeData runtimeData)
        {
            _context = contexts.game;
            _staticData = staticData;
            _runtimeData = runtimeData;
        }

        public void Initialize()
        {
            TankSpawnerData[] tankSpawners = _staticData.TankSpawners.ToArray();
            Vector3 centralPoint =
                tankSpawners.Aggregate(Vector3.zero, (current, spawner) => current + spawner.Position) / tankSpawners.Length;
            
            foreach (var tankSpawner in tankSpawners)
            {
                var tankEntity = CreateTankEntity(tankSpawner, centralPoint);
                var tankView = _context.viewService.value.CreateView(_staticData.TankPrefab, tankEntity);
                SetTankEntity(tankEntity, tankView, tankSpawner.Type);
            }
        }

        private GameEntity CreateTankEntity(TankSpawnerData tankSpawner, Vector3 centralPoint)
        {
            var tankEntity = _context.CreateEntity();
            tankEntity.AddPosition(tankSpawner.Position);
            tankEntity.AddRotation(Quaternion.LookRotation(centralPoint - tankSpawner.Position));
            return tankEntity;
        }

        private void SetTankEntity(GameEntity tankEntity, IView tankView, TeamType team)
        {
            tankEntity.SetHealth(100);
            tankEntity.AddTeam(team);
            tankEntity.AddTransform(tankView.Transform);
            tankEntity.AddRigidbody(tankView.GameObject.GetComponent<Rigidbody>());
            tankEntity.AddMeshRenderer(tankView.GameObject.GetComponentsInChildren<MeshRenderer>());
            tankEntity.AddWeaponTransform(tankView.Transform.Find(c_fireTransform));
            tankEntity.AddWeaponAmmo(_runtimeData.Shell);
        }
    }
}