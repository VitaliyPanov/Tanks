using Tanks.Core.GameControllers;
using Tanks.Core.Infrastructure.StateMachine.Interfaces;
using UnityEngine;

namespace Tanks.Core.Infrastructure.StateMachine.Game
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private GeneralController _gameController;

        public GameLoopState(GameStateMachine stateMachine) => _stateMachine = stateMachine;

        public void Enter()
        {
            _gameController = Object.FindObjectOfType<GeneralController>();
            Debug.Log(_gameController.name);
        }

        public void Exit() {}
    }
}