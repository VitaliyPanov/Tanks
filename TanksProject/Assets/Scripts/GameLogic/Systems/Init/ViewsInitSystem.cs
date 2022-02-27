using Entitas;

namespace Tanks.GameLogic.Systems.Init
{
    public sealed class ViewsInitSystem : IInitializeSystem
    {
        private readonly Contexts _contexts;

        public ViewsInitSystem(Contexts contexts) => _contexts = contexts;

        public void Initialize()
        {
            foreach (var entity in _contexts.game.GetGroup(GameMatcher.View))
            {
                entity.view.Value.InitializeView(entity);
            }
        }
    }
}