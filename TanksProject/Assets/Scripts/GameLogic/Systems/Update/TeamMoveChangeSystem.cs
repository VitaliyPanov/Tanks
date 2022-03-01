using System.Collections.Generic;
using System.Linq;
using Entitas;
using Tanks.Data;
using Tanks.GameLogic.Services;
using Tanks.General.Controllers;
using UnityEngine;

namespace Tanks.GameLogic.Systems.Update
{
    internal sealed class TeamMoveChangeSystem : ReactiveSystem<GameEntity>, IInitializeSystem
    {
        private const int c_maxAngle = 40;
        private readonly RuntimeData _runtimeData;
        private readonly IControllersMediator _mediator;
        private readonly IGroup<GameEntity> _entities;
        private readonly IGroup<GameEntity> _movableEntities;
        private readonly Contexts _contexts;
        private List<GameEntity> _buffer = new();
        private List<TeamType> _activeTeams = new();
        private TeamType _currentTeam;

        public TeamMoveChangeSystem(Contexts contexts, RuntimeData runtimeData, IControllersMediator mediator) : base(contexts.game)
        {
            _runtimeData = runtimeData;
            _mediator = mediator;
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
            GameEntity firstMovable = FirstMovable(_currentTeam);
            if (firstMovable != null)
            {
                if (_contexts.game.controllable.Entity == entities[0]) 
                    firstMovable.tryControl = true;
            }
            else
                ChangeMovableTeam(_activeTeams.GetNext(_currentTeam));
        }

        private GameEntity FirstMovable(TeamType team)
        {
            foreach (var movableEntity in _movableEntities)
            {
                if (movableEntity.team.Type == team)
                    return movableEntity;
            }
            return null;
        }

        private void ChangeMovableTeam(TeamType team)
        {
            for (int i = 0; i < _activeTeams.Count; i++)
            {
                ReplaceMovableTeam(team);
                GameEntity firstMovable = FirstMovable(team);
                if (firstMovable != null)
                {
                    firstMovable.tryControl = true;
                    break;
                }

                TeamType previous = _activeTeams.GetPrevious(team);
                _activeTeams.Remove(team);
                team = _activeTeams.GetNext(previous);
            }
            _mediator.ChangeTeam(team);
            _currentTeam = team;
        }

        private void ReplaceMovableTeam(TeamType team)
        {
            foreach (var entity in _entities.GetEntities(_buffer))
            {
                if (entity.team.Type == team)
                {
                    SetUnitMovableAndAbleToFire(entity);
                    CheckRotation(entity.transform.Value);
                    _contexts.game.SetTimer(_runtimeData.MoveTime, GameComponentsLookup.Movable, entity);
                }
            }
        }

        private void SetUnitMovableAndAbleToFire(GameEntity tankEntity)
        {
            tankEntity.isMovable = true;
            tankEntity.isWeaponFired = false;
        }

        private void CheckRotation(Transform tank)
        {
            if (Mathf.Abs(tank.rotation.eulerAngles.x) > c_maxAngle || Mathf.Abs(tank.rotation.eulerAngles.z) > c_maxAngle)
                tank.SetPositionAndRotation(tank.position += Vector3.up, Quaternion.identity);
        }
    }
}