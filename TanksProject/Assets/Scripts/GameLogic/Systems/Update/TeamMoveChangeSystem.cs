using System.Collections.Generic;
using System.Linq;
using Entitas;
using Tanks.Data;
using Tanks.GameLogic.Services;

namespace Tanks.GameLogic.Systems.Update
{
    internal sealed class TeamMoveChangeSystem : ReactiveSystem<GameEntity>, IInitializeSystem
    {
        private readonly RuntimeData _runtimeData;
        private readonly IGroup<GameEntity> _entities;
        private readonly IGroup<GameEntity> _movableEntities;
        private readonly Contexts _contexts;
        private List<GameEntity> _buffer = new();
        private List<TeamType> _activeTeams = new();

        public TeamMoveChangeSystem(Contexts contexts, RuntimeData runtimeData) : base(contexts.game)
        {
            _runtimeData = runtimeData;
            _contexts = contexts;
            _entities = contexts.game.GetGroup(GameMatcher.Team);
            _movableEntities = contexts.game.GetGroup(GameMatcher.Movable);
        }

        public void Initialize()
        {
            _activeTeams = _entities.GetEntities(_buffer).Select(e => e.team.Type).Distinct().ToList();
            _contexts.game.SetControllable(_buffer.First());
            ChangeMovableTeam(_runtimeData.CurrentTeamMove);
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.Movable.Removed());

        protected override bool Filter(GameEntity entity) => true;

        protected override void Execute(List<GameEntity> entities)
        {
            GameEntity firstMovable = FirstMovable(entities);
            if (firstMovable != null)
            {
                if (_contexts.game.controllable.Entity == entities[0]) 
                    firstMovable.tryControl = true;
            }
            else
                ChangeMovableTeam(_activeTeams.GetNext(entities[0].team.Type));
        }

        private GameEntity FirstMovable(List<GameEntity> entities)
        {
            GameEntity firstMovable = null;
            
            foreach (var movableEntity in _movableEntities)
            {
                if (movableEntity.team.Type == entities.First().team.Type && firstMovable == null)
                    firstMovable = movableEntity;
            }

            return firstMovable;
        }

        private void ChangeMovableTeam(TeamType team)
        {
            for (int i = 0; i < _activeTeams.Count; i++)
            {
                int? alive = CheckAliveInTeamAndSetMovable(team, null);
                if (alive != null)
                {
                    _buffer[(int) alive].tryControl = true;
                    break;
                }

                TeamType previous = _activeTeams.GetPrevious(team);
                _activeTeams.Remove(team);
                team = _activeTeams.GetNext(previous);
            }

            _runtimeData.ChangeTeam(team);
        }

        private int? CheckAliveInTeamAndSetMovable(TeamType team, int? firstAlive)
        {
            for (int i = 0; i < _entities.GetEntities(_buffer).Count; i++)
            {
                GameEntity tankEntity = _buffer[i];
                if (tankEntity.team.Type == team)
                {
                    SetUnitMovableAndAbleToFire(tankEntity);
                    firstAlive ??= i;
                }
            }

            return firstAlive;
        }

        private void SetUnitMovableAndAbleToFire(GameEntity tankEntity)
        {
            tankEntity.isMovable = true;
            tankEntity.isWeaponFired = false;
            _contexts.game.SetTimer(_runtimeData.MoveTime, GameComponentsLookup.Movable, tankEntity);
        }
    }
}