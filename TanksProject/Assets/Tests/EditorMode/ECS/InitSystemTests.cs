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
    public class UpdateSystemTests
    {
        private const string c_mainScene = "MainScene";
        private Contexts _contexts;
        private IDataService _dataService;

        [SetUp]
        public void Init()
        {
            SceneStaticData staticData = Resources.Load<SceneStaticData>(DataPaths.SCENE + "/MainSceneStaticData");
            RuntimeData runtimeData = Resources.Load<RuntimeData>(DataPaths.RUNTIME + "/RuntimeData");
            AmmoData shellData = Resources.Load<AmmoData>(DataPaths.AMMO + "/Shell");

            _dataService = Substitute.For<IDataService>();
            _dataService.StaticData(c_mainScene).Returns(staticData);
            _dataService.RuntimeData.Returns(runtimeData);
            _dataService.AmmunitionData(AmmoType.Shell).Returns(shellData);

            _contexts = new Contexts();
            _contexts.game.SetViewService(new ViewService(new PoolService()));
        }
        
        [Test]
        public void WhenTankInitSystem_AndMainScene_ThenTeamEntitiesShouldBeEqualSpawnersCount()
        {
            // Arrange.
            var staticData = _dataService.StaticData(c_mainScene);
            var system = new TanksInitSystem(_contexts, staticData, _dataService.RuntimeData);
            IGroup<GameEntity> entities = _contexts.game.GetGroup(GameMatcher.Team);
            // Act.
            system.Initialize();
            
            var teamsAtSystem = entities.GetEntities().Select(e => e.team.Type).Distinct().ToList();
            var teamsAtData = staticData.TankSpawners.Select(t => t.Type).Distinct().ToList();
            // Assert.
            entities.count.Should().Be(staticData.TankSpawners.Count);
            teamsAtSystem.Count.Should().Be(teamsAtData.Count);
        }

    }
}