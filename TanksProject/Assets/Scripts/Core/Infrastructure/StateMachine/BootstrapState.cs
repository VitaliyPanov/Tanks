namespace TanksGB.Core.Infrastructure.StateMachine
{
    public sealed class BootstrapState : IState
    {
        private readonly GameStateMachine _stateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly string _startScene;

        public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader, string startScene)
        {
            _stateMachine = stateMachine;
            _sceneLoader = sceneLoader;
            _startScene = startScene;
        }

        public void Enter() => _sceneLoader.Load(SceneNames.LOADING, EnterSceneLoadingState);

        private void EnterSceneLoadingState() => _stateMachine.Enter<LoadSceneState, string>(_startScene);

        public void Exit()
        {
        }
    }
}