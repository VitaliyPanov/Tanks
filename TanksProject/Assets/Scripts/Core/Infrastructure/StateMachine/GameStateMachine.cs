using System;
using System.Collections.Generic;
using General.Services;
using General.UI;

namespace Tanks.Core.Infrastructure.StateMachine
{
    public sealed class GameStateMachine
    {
        private readonly Dictionary<Type, IExitableState> _states;
        private IExitableState _currentState;

        public GameStateMachine(SceneLoader sceneLoader, string startScene, LoadingScreen loadingCurtain, IGameFactory gameFactory)
        {
            _states = new Dictionary<Type, IExitableState>()
            {
                [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, startScene),
                [typeof(LoadSceneState)] = new LoadSceneState(this, sceneLoader, loadingCurtain, gameFactory),
                [typeof(GameLoopState)] = new GameLoopState(this)
            }; 
            //TODO: Dispose for states and services
        }

        public void Enter<TState>() where TState : class, IState
        {
            TState state = ChangeState<TState>();
            state.Enter();
        }

        public void Enter<TState, TLoad>(TLoad sceneName) where TState : class, ILoadingState<TLoad>
        {
            TState state = ChangeState<TState>();
            state.Enter(sceneName);
        }

        private TState ChangeState<TState>() where TState : class, IExitableState
        {
            _currentState?.Exit();
            TState state = GetState<TState>();
            _currentState = state;
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => _states[typeof(TState)] as TState;
    }
}