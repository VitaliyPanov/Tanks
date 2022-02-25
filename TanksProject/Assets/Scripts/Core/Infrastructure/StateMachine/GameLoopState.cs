using Tanks.Core.Controllers;
using UnityEngine;

namespace Tanks.Core.Infrastructure.StateMachine
{
    public class GameLoopState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private GameController _gameController;

        public GameLoopState(GameStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            _gameController = Object.FindObjectOfType<GameController>();
            Debug.Log(_gameController.name);
        }

        public void Exit()
        {
        }
    }
}