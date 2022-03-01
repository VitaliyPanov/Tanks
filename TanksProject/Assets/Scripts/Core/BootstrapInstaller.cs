using Tanks.Core.GameControllers;
using Tanks.Core.Infrastructure.Services;
using Tanks.Core.Infrastructure.Services.Input;
using Tanks.Core.Infrastructure.Services.Pool;
using Tanks.GameLogic;
using Tanks.General.Controllers;
using Tanks.General.Services;
using Tanks.UI;
using UnityEngine;
using Zenject;

namespace Tanks.Core
{
    internal sealed class BootstrapInstaller:MonoInstaller
    {
        public override void InstallBindings()
        {
            BindGeneralServices();
            BindControllers();
        }

        private void BindGeneralServices()
        {
            Container.Bind<IInputService>().To<InputService>().AsSingle();
            Container.Bind<IGameFactory>().To<GameFactory>().AsSingle();
            Container.Bind<IDataService>().To<DataService>().AsSingle();
            Container.Bind<IPoolService>().To<PoolService>().AsSingle();
            Container.Bind<ITimeService>().To<TimeService>().AsSingle();
        }

        private void BindControllers()
        {
            Container.Bind<IControllersMediator>().To<Mediator>().FromInstance(new GameObject("[MEDIATOR]").AddComponent<Mediator>());
            
            Container.Bind<ILogicController>().To<GameLogicController>().AsSingle();
            Container.Bind<ICameraController>().To<CameraController>().AsSingle();
            Container.Bind<IUIController>().To<UIController>().AsSingle();
        }
    }
}