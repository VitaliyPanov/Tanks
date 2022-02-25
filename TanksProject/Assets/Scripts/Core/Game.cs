using General.Services;
using General.UI;
using Tanks.Core.Infrastructure.StateMachine;

namespace Tanks.Core
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