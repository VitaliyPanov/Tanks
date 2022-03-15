using Tanks.Core.Infrastructure.StateMachine;
using Tanks.Core.Infrastructure.StateMachine.Game;
using Tanks.General.Services;
using Tanks.General.UI;
using UnityEngine;
using Zenject;

namespace Tanks.Core
{
    internal sealed class Bootstrapper : MonoBehaviour
    {
        [SerializeField] private LoadingScreen _loadingScreenPrefab;
        private Game _game;
        private void Awake()
        {
            CreateServices();
            DontDestroyOnLoad(this);
        }

        [Inject]
        private void OnContextRun(IGameFactory gameFactory)
        {
            _game = new Game(SceneNames.MAIN, Instantiate(_loadingScreenPrefab), gameFactory);
            _game.StateMachine.Start();
        }

        private void CreateServices()
        {
            SceneContext zenjectBootstrapContext = GetComponent<SceneContext>();
            zenjectBootstrapContext.Run();
        }
        
    }
}