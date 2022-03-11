using Tanks.Core.GameControllers;
using Tanks.General.Controllers;
using Tanks.General.Services;
using UnityEngine;

namespace Tanks.Core.Infrastructure.Services
{
    internal sealed class GameFactory : IGameFactory
    {
        private readonly IDataService _dataService;
        private readonly ILogicController _logicController;
        private readonly ICameraController _cameraController;
        private readonly IUIController _uiController;
        private readonly Mediator _mediator;
        private const string c_gameController = "[GAMECONTROLLER]";

        public GameFactory(IDataService dataService, ILogicController logicController,
            ICameraController cameraController, IUIController uiController, IControllersMediator mediator)
        {
            _dataService = dataService;
            _logicController = logicController;
            _cameraController = cameraController;
            _uiController = uiController;
            _mediator = (Mediator) mediator;

            _dataService.Load();
        }

        public void CreateGameController(string sceneName)
        {
            GameObject gameController = new GameObject(c_gameController);
            gameController.AddComponent<GeneralController>().Construct(_logicController, _cameraController, _uiController,
                _dataService, _mediator, sceneName);
        }
    }
}