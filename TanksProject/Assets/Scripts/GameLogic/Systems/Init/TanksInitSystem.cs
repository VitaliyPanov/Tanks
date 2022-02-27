using System.Linq;
using Entitas;
using Tanks.Data;
using Tanks.GameLogic.Services;
using Tanks.GameLogic.Views.Behaviours;
using UnityEngine;
using UnityEngine.AI;

namespace Tanks.GameLogic.Systems.Init
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
                tankSpawners.Aggregate(Vector3.zero, (current, spawner) => current + spawner.Position) /
                tankSpawners.Length;

            foreach (var tankSpawner in tankSpawners)
            {
                var tankEntity = CreateTankEntity(tankSpawner.Type, tankSpawner.Position,
                    Quaternion.LookRotation(centralPoint - tankSpawner.Position));
                CreateTankView(tankEntity, _staticData.PlayableTeam, _staticData.StartTankHealth,
                    _runtimeData.Shell);
            }
        }

        private GameEntity CreateTankEntity(TeamType team, Vector3 position, Quaternion rotation)
        {
            var tankEntity = _context.CreateEntity();
            tankEntity.AddTeam(team);
            tankEntity.AddPosition(position);
            tankEntity.AddRotation(rotation);
            return tankEntity;
        }

        private void CreateTankView(GameEntity tankEntity, TeamType playableTeam, float health, AmmoData ammoData)
        {
            var tankView = _context.viewService.value.CreateView(_staticData.TankPrefab);
            if (tankEntity.team.Type == playableTeam)
                tankEntity.isPlayable = true;
            else
            {
                tankView.GameObject.GetOrAddComponent<NavMeshAgentBehaviour>();
            }

            tankEntity.SetHealth(health);
            tankEntity.AddWeaponAmmo(ammoData);
            tankEntity.AddTransform(tankView.Transform);
            tankEntity.AddRigidbody(tankView.GameObject.GetComponent<Rigidbody>());
            tankEntity.AddMeshRenderer(tankView.GameObject.GetComponentsInChildren<MeshRenderer>());
            tankEntity.AddWeaponTransform(tankView.Transform.Find(c_fireTransform));
            tankView.InitializeView(tankEntity);
        }
    }
}