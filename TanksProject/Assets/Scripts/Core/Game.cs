using General.Services;
using General.UI;
using TanksGB.Core.Infrastructure.StateMachine;

namespace TanksGB.Core
{
    internal sealed class Game
    {
        public readonly GameStateMachine StateMachine;
        
        public Game(string startScene, LoadingScreen loadingCurtain, IGameFactory gameFactory)
        {
            StateMachine = new GameStateMachine(new SceneLoader(), startScene, loadingCurtain, gameFactory);
        }
    }
}