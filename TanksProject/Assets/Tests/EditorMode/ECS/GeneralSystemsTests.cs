using System.Collections.Generic;
using System.Linq;
using Entitas;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Tanks.Data;
using Tanks.GameLogic.Services;
using Tanks.GameLogic.Services.View;
using Tanks.GameLogic.Systems.Init;
using Tanks.GameLogic.Systems.Update;
using Tanks.GameLogic.Views;
using Tanks.General.Controllers;
using Tanks.General.Services;
using UnityEngine;

namespace Tanks.Tests.EditorMode.ECS
{
    public class GeneralSystemsTests
    {
        private Contexts _contexts;
        private IDataService _dataService;
        private IPoolService _poolService;
        private IViewService _viewService;

        [SetUp]
        public void Init()
        {
            var staticData = TestsSetups.InstantiateStaticData();
            var shellData = TestsSetups.InstantiateShellData();
            var runtimeData = TestsSetups.InstantiateRuntimeData(staticData, shellData);

            _dataService = new MockRepository(MockBehavior.Default)
                .Of<IDataService>()
                .Where(m => m.StaticData(It.IsAny<string>()) == staticData)
                .Where(m => m.RuntimeData == runtimeData)
                .First(m => m.AmmunitionData(AmmoType.Shell) == shellData);

            _viewService = Mock.Of<IViewService>();

            _contexts = new Contexts();
            _contexts.game.SetViewService(_viewService);
        }

        [Test]
        public void WhenTankInitSystemInitialize_AndTeamsAndTanksAre3_ThenTeamEntitiesShouldBeEqualSpawnersCount()
        {
            // Arrange.
            var staticData = _dataService.StaticData("");
            staticData.TankSpawners = new List<TankSpawnerData>
            {
                new(TeamType.Blue, new Vector3(1, 0, 1)),
                new(TeamType.Red, new Vector3(10, 0, 10)),
                new(TeamType.Black, new Vector3(15, 0, 15)),
            };
            Mock.Get(_viewService)
                .Setup(m => m.CreateView(It.IsAny<GameObject>(), null))
                .Returns(() => new GameObject().AddComponent<UnityView>());

            var system = new TanksInitSystem(_contexts.game, _contexts.aI, staticData, _dataService.RuntimeData);
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
        public void WhenHealthControlSystemExecute_AndCurrentHealthIsZero_ThenEntityIsDead()
        {
            // Arrange.
            var system = new HealthControlSystem(_contexts.game);
            var entity = _contexts.game.CreateEntity();
            // Act.
            entity.AddCurrentHealth(0);
            system.Execute();
            // Assert.
            entity.isDead.Should().BeTrue();
        }

        [Test]
        public void WhenDestroySystemCleanup_AndEntityIsDestroy_ThenEntityIsNotEnabled()
        {
            // Arrange.
            var system = new DestroySystem(_contexts.game);
            var entity = _contexts.game.CreateEntity();
            // Act.
            entity.isDestroy = true;
            system.Cleanup();
            // Assert.
            entity.isEnabled.Should().BeFalse();
        }

        [Test]
        public void WhenViewDeadActivateSystemExecute_AndEntityIsDead_ThenCreatesExplosion()
        {
            // Arrange.
            var poolService = Mock.Of<IPoolService>(m =>
                m.Instantiate<ParticleSystem>(It.IsAny<GameObject>(), It.IsAny<Transform>()) ==
                new GameObject("Explosion").AddComponent<ParticleSystem>());

            var mediator = Mock.Of<IControllersMediator>();
            var view = Mock.Of<IView>(m => m.Transform == new GameObject().transform);

            var system = new ViewDeadActivateSystem(_contexts.game, _dataService.StaticData(""), poolService, mediator);
            var deadEntity = _contexts.game.CreateEntity();
            var explosionEntities = _contexts.game.GetGroup(GameMatcher.Particle);

            // Act.
            deadEntity.AddView(view);
            deadEntity.AddTransform(view.Transform);
            deadEntity.isDead = true;
            system.Execute();
            // Assert.
            explosionEntities.GetEntities().Length.Should().Be(1);
        }

        [Test]
        public void WhenTimeTrippingSystemExecute_AndEntityHasTimerZeroOnComponent_ThenComponentDeleted()
        {
            // Arrange.
            var system = new TimeTrippingSystem(_contexts.game, _contexts.input);
            var entity = _contexts.game.CreateEntity();
            // Act.
            entity.AddPosition(new Vector3(0, 0, 0));
            _contexts.input.ReplaceFixedDeltaTime(0);
            _contexts.game.SetTimer(entity, GameComponentsLookup.Position, 0);
            system.Execute();
            // Assert.
            entity.hasPosition.Should().BeFalse();
        }
        
        
    }
}