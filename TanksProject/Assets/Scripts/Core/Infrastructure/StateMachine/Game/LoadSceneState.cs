using System;
using Tanks.Core.Infrastructure.StateMachine.Interfaces;
using Tanks.General.Services;
using Tanks.General.UI;
using UnityEngine;

namespace Tanks.Core.Infrastructure.StateMachine.Game
{
    public sealed class LoadSceneState : ILoadingState<string>
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly LoadingScreen _loadingCurtain;
        private readonly IGameFactory _gameFactory;
        private string _sceneName;

        public LoadSceneState(GameStateMachine stateMachine, SceneLoader sceneLoader, LoadingScreen loadingCurtain,
            IGameFactory gameFactory)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _loadingCurtain = loadingCurtain;
            _gameFactory = gameFactory;
        }

        public void Enter(string scene)
        {
            _sceneName = scene;
            _loadingCurtain.Show();
            _sceneLoader.Load(scene, OnSceneLoaded);
        }

        private void OnSceneLoaded()
        {
            _gameFactory.CreateGameController(_sceneName);
            _stateMachine.EnterGameLoop();
        }

        public void Exit() => _loadingCurtain.Hide();
    }
}