using Entitas;
using TanksGB.Data;

namespace TanksGB.GameLogic.Systems.Init
{
    internal sealed class TeamsInitSystem : IInitializeSystem
    {
        private readonly IGroup<GameEntity> _teamEntities;

        public TeamsInitSystem(Contexts contexts) => _teamEntities =
            contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.Team, GameMatcher.MeshRenderer));

        public void Initialize()
        {
            foreach (var entity in _teamEntities)
            {
                foreach (var renderer in entity.meshRenderer.Array)
                {
                    renderer.material.color = TeamColors.TeamColor(entity.team.Type);
                }
            }
        }
    }
}