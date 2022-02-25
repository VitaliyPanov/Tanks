using General.Controllers;
using General.Services;
using TanksGB.Core.Controllers;
using TanksGB.Core.Infrastructure.Services;
using TanksGB.Core.Infrastructure.Services.Input;
using TanksGB.Core.Infrastructure.Services.Pool;
using TanksGB.GameLogic;
using TanksGB.GameLogic.Services;
using TanksGB.UI;
using Zenject;

namespace TanksGB.Core
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
            Container.Bind<ILogicController>().To<GameLogicController>().AsSingle();
            Container.Bind<ICameraController>().To<CameraController>().AsSingle();
            Container.Bind<IUIController>().To<UIController>().AsSingle();
        }
    }
}