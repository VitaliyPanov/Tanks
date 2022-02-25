using General.Controllers;
using General.Services;
using Tanks.Core.Controllers;
using UnityEngine;

namespace Tanks.Core.Infrastructure.Services
{
    internal sealed class GameFactory : IGameFactory
    {
        private readonly IDataService _dataService;
        private readonly ILogicController _logicController;
        private readonly ICameraController _cameraController;
        private readonly IUIController _uiController;
        private const string c_gameController = "GameController";

        public GameFactory(IDataService dataService, ILogicController logicController,
            ICameraController cameraController, IUIController uiController)
        {
            _dataService = dataService;
            _logicController = logicController;
            _cameraController = cameraController;
            _uiController = uiController;

            _dataService.Load();
        }

        public void CreateGameController(string sceneName)
        {
            GameObject gameController = new GameObject(c_gameController);
            gameController.AddComponent<GameController>().Construct(_logicController, _cameraController, _uiController,
                _dataService, sceneName);
        }
    }
}