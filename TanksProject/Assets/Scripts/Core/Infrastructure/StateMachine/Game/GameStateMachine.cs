using Tanks.General.Services;
using Tanks.General.UI;

namespace Tanks.Core.Infrastructure.StateMachine.Game
{
    public sealed class GameStateMachine : StateMachine
    {
        public GameStateMachine(SceneLoader sceneLoader, string startScene, LoadingScreen loadingCurtain, IGameFactory gameFactory)
        {
            States[typeof(BootstrapState)] = new BootstrapState(this, sceneLoader, startScene);
            States[typeof(LoadSceneState)] = new LoadSceneState(this, sceneLoader, loadingCurtain, gameFactory);
            States[typeof(GameLoopState)] = new GameLoopState(this);
        }
        public void Start() => Enter<BootstrapState>();
        public void LoadScene(string sceneName) => Enter<LoadSceneState, string>(sceneName);
        public void EnterGameLoop() => Enter<GameLoopState>();
    }
}