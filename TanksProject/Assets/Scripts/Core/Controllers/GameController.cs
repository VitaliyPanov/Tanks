using General.Controllers;
using General.Services;
using UnityEngine;

namespace TanksGB.Core.Controllers
{
    internal sealed class GameController : MonoBehaviour
    {
        private IDataService _dataService;
        private ILogicController _logicController;
        private ICameraController _cameraController;
        private IUIController _uiController;
        private Controllers _controllers;

        public void Construct(ILogicController logicController, ICameraController cameraController,
            IUIController uiController, IDataService dataService, string sceneName)
        {
            _dataService = dataService;
            _logicController = logicController;
            _cameraController = cameraController;
            _uiController = uiController;
            
            _logicController.Initialize(sceneName);
            _cameraController.SetCamera(Camera.main);
            
            _dataService.RuntimeData.OnTeamMoveChangedEvent += PauseLogic;
            _dataService.RuntimeData.OnControllableTransformChangedEvent += ReplaceCameraTarget;
            _uiController.OnMessageHideEvent += UnPauseLogic;
            
            _controllers = new Controllers();
            _controllers
                .Add(_logicController)
                .Add(_cameraController)
                .Add(_uiController);
        }

        private void ReplaceCameraTarget(Transform target) => _cameraController.SetTarget(target);

        private void Start() => _controllers.Start();

        private void Update() => _controllers.Update();

        private void FixedUpdate() => _controllers.FixedUpdate();

        private void LateUpdate() => _controllers.LateUpdate();

        private void PauseLogic()
        {
            _logicController.Pause(true);
            _uiController.ShowMessage(_dataService.RuntimeData.CurrentTeamMove.ToString(),
                _dataService.RuntimeData.MoveTime);
        }

        private void UnPauseLogic() => _logicController.Pause(false);

        private void OnDestroy()
        {
            _dataService.RuntimeData.OnTeamMoveChangedEvent -= PauseLogic;
            _dataService.RuntimeData.OnControllableTransformChangedEvent -= ReplaceCameraTarget;
            _uiController.OnMessageHideEvent -= UnPauseLogic;
            _controllers.Destroy();
        }
    }
}