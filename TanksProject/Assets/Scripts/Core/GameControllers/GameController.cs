using Tanks.General.Controllers;
using Tanks.General.Services;
using UnityEngine;

namespace Tanks.Core.GameControllers
{
    internal sealed class GameController : MonoBehaviour
    {
        private IDataService _dataService;
        private ILogicController _logicController;
        private ICameraController _cameraController;
        private IUIController _uiController;
        private Controllers _controllers;
        private Mediator _mediator;

        private void Awake() => DontDestroyOnLoad(this);

        public void Construct(ILogicController logicController, ICameraController cameraController,
            IUIController uiController, IDataService dataService, Mediator mediator, string sceneName)
        {
            _dataService = dataService;
            _logicController = logicController;
            _cameraController = cameraController;
            _uiController = uiController;
            _mediator = mediator;
            
            _mediator.Construct(_logicController, _cameraController, _uiController, _dataService.RuntimeData);
            
            InitializeControllers(sceneName);
            AddControllersToList();
        }

        private void InitializeControllers(string sceneName)
        {
            _logicController.Initialize(_dataService.StaticData(sceneName), _dataService.RuntimeData);
            _cameraController.Initialize(Camera.main);
            _uiController.Initialize(_dataService.UIData);
        }

        private void AddControllersToList()
        {
            _controllers = new Controllers();
            _controllers
                .Add(_logicController)
                .Add(_cameraController)
                .Add(_uiController);
        }

        private void Start() => _controllers.Start();

        private void Update() => _controllers.Update();

        private void FixedUpdate() => _controllers.FixedUpdate();

        private void LateUpdate() => _controllers.LateUpdate();

        private void OnDestroy() => _controllers.Destroy();
    }
}