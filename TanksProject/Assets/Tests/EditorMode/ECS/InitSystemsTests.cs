using System.Collections.Generic;
using System.Linq;
using Entitas;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Tanks.Core.Infrastructure.Services.Pool;
using Tanks.Data;
using Tanks.GameLogic.Services.View;
using Tanks.GameLogic.Systems.Init;
using Tanks.General.Services;
using UnityEngine;

namespace Tanks.Tests.EditorMode.ECS
{
    public class InitSystemsTests
    {
        private Contexts _contexts;
        private IDataService _dataService;

        private static RuntimeData InstantiateRuntimeData(SceneStaticData staticData, AmmoData shellData)
        {
            RuntimeData runtimeData = ScriptableObject.CreateInstance<RuntimeData>();
            runtimeData.MovementSpeed = 100;
            runtimeData.TurnSpeed = 100;
            runtimeData.MoveTime = 100;
            runtimeData.CurrentTeamMove = staticData.FirstMoveTeam;
            runtimeData.Shell = shellData;
            return runtimeData;
        }

        private static SceneStaticData InstantiateStaticData()
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

        private static AmmoData InstantiateShellData()
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

        [SetUp]
        public void Init()
        {
            var staticData = InstantiateStaticData();
            var shellData = InstantiateShellData();
            var runtimeData = InstantiateRuntimeData(staticData, shellData);
            
            _dataService = Substitute.For<IDataService>();
            _dataService.StaticData("").Returns(staticData);
            _dataService.RuntimeData.Returns(runtimeData);
            _dataService.AmmunitionData(AmmoType.Shell).Returns(shellData);
            
            _contexts = new Contexts();
            _contexts.game.SetViewService(new ViewService(new PoolService()));
        }

        [Test]
        public void WhenTankInitSystem_AndTeamsAndTanksAre3_ThenTeamEntitiesShouldBeEqualSpawnersCount()
        {
            // Arrange.
            var staticData = _dataService.StaticData("");
            staticData.TankSpawners = new List<TankSpawnerData>
            {
                new(TeamType.Blue, new Vector3(1, 0, 1)),
                new(TeamType.Red, new Vector3(10, 0, 10)),
                new(TeamType.Black, new Vector3(15, 0, 15)),
            };
            int spawnersCount = staticData.TankSpawners.Count;
            var system = new TanksInitSystem(_contexts, staticData, _dataService.RuntimeData);
            IGroup<GameEntity> entities = _contexts.game.GetGroup(GameMatcher.Team);
            // Act.
            system.Initialize();
            var teamsCountAtSystem = entities.GetEntities().Select(e => e.team.Type).Distinct().ToList().Count;
            var teamsCountAtData = staticData.TankSpawners.Select(t => t.Type).Distinct().ToList().Count;
            // Assert.
            entities.count.Should().Be(spawnersCount);
            teamsCountAtSystem.Should().Be(teamsCountAtData);
        }
    }
}