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
using Tanks.GameLogic.Systems.Update;
using Tanks.General.Services;
using UnityEngine;

namespace Tanks.Tests.EditorMode.ECS
{
    public class InitSystemsTests
    {
        private Contexts _contexts;
        private IDataService _dataService;

        [SetUp]
        public void Init()
        {
            var staticData = TestsSetups.InstantiateStaticData();
            var shellData = TestsSetups.InstantiateShellData();
            var runtimeData = TestsSetups.InstantiateRuntimeData(staticData, shellData);
            
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
            var system = new TanksInitSystem(_contexts, staticData, _dataService.RuntimeData);
            IGroup<GameEntity> entities = _contexts.game.GetGroup(GameMatcher.Team);
            // Act.
            system.Initialize();
            var teamsCountAtSystem = entities.GetEntities().Select(e => e.team.Type).Distinct().ToList().Count;
            var teamsCountAtData = staticData.TankSpawners.Select(t => t.Type).Distinct().ToList().Count;
            int spawnersCount = staticData.TankSpawners.Count;
            // Assert.
            entities.count.Should().Be(spawnersCount);
            teamsCountAtSystem.Should().Be(teamsCountAtData);
        }

        [Test]
        public void WhenHealthControlSystem_AndCurrentHealthIsZero_ThenEntityShouldBeDead()
        {
            // Arrange.
            var system = new HealthControlSystem(_contexts);
            var entity = _contexts.game.CreateEntity();
            // Act.
            entity.AddCurrentHealth(0);
            system.Execute();
            // Assert.
            entity.isDead.Should().BeTrue();
        }
        
    }
}