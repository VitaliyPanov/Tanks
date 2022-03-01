using Tanks.Core.Infrastructure.StateMachine;
using Tanks.General.Services;
using Tanks.General.UI;

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