using Tanks.Data;
using Tanks.General.Controllers;
using UnityEngine;

namespace Tanks.Core.GameControllers
{
    internal sealed class Mediator : MonoBehaviour, IControllersMediator
    {
        private ILogicController _logicController;
        private ICameraController _cameraController;
        private IUIController _uiController;
        private RuntimeData _runtimeData;

        private void Awake() => DontDestroyOnLoad(this);

        public void Construct(ILogicController logicController, ICameraController cameraController,
            IUIController uiController, RuntimeData runtimeData)
        {
            _logicController = logicController;
            _cameraController = cameraController;
            _uiController = uiController;
            _runtimeData = runtimeData;
        }

        public void ReplaceControllable(Transform target, string id)
        {
            _runtimeData.Controllable = target;
            _cameraController.SetTarget(target);
            _uiController.SetPlayer(target, id);
        }

        public void OnDestroyView(Transform target, string id) => _uiController.RemoveMapElement(id);

        public async void ChangeTeam(TeamType team)
        {
            _logicController.Pause(true);
            _runtimeData.CurrentTeamMove = team;
            await _uiController.ShowTeamMove(team, _runtimeData.MoveTime);
            _logicController.Pause(false);
        }

        public void SetWinner(TeamType team) => _uiController.ShowWinner(team);
    }
}