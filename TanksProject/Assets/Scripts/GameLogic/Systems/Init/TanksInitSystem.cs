using System.Linq;
using Entitas;
using Tanks.Data;
using Tanks.GameLogic.Services;
using Tanks.GameLogic.Views.Behaviours;
using UnityEngine;

namespace Tanks.GameLogic.Systems.Init
{
    public sealed class TanksInitSystem : IInitializeSystem
    {
        private const string c_fireTransform = "FireTransform";
        private readonly GameContext _gameContext;
        private readonly AIContext _aiContext;
        private readonly SceneStaticData _staticData;
        private readonly RuntimeData _runtimeData;

        public TanksInitSystem(GameContext gameContext, AIContext aiContext, SceneStaticData staticData,
            RuntimeData runtimeData)
        {
            _gameContext = gameContext;
            _aiContext = aiContext;
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
            var tankEntity = _gameContext.CreateEntity();
            tankEntity.AddTeam(team);
            tankEntity.AddPosition(position);
            tankEntity.AddRotation(rotation);
            return tankEntity;
        }

        private void CreateTankView(GameEntity tankEntity, TeamType playableTeam, float health, AmmoData ammoData)
        {
            var tankView = _gameContext.viewService.value.CreateView(_staticData.TankPrefab, null);
            if (tankEntity.team.Type == playableTeam)
                tankEntity.isPlayable = true;
            else
            {
                tankEntity.isAI = true;
                tankView.GameObject.GetOrAddComponent<NavMeshAgentBehaviour>().Construct(_aiContext.CreateEntity());
            }

            tankView.GameObject.GetOrAddComponent<HealthBehaviour>();
            tankView.GameObject.GetOrAddComponent<WeaponBehaviour>();
            tankEntity.AddMaxHealth(health);
            tankEntity.AddWeaponAmmo(ammoData);
            tankEntity.AddTransform(tankView.Transform);
            tankEntity.AddRigidbody(tankView.GameObject.GetComponent<Rigidbody>());
            tankEntity.AddMeshRenderer(tankView.GameObject.GetComponentsInChildren<MeshRenderer>());
            tankEntity.AddWeaponTransform(tankView.Transform.Find(c_fireTransform));
            tankView.InitializeView(tankEntity);
        }
    }
}