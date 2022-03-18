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
        private readonly GameContext _context;
        private readonly List<GameEntity> _buffer = new();
        private List<TeamType> _activeTeams = new();
        private TeamType _currentTeam;

        public TeamMoveChangeSystem(GameContext gameContext, RuntimeData runtimeData, IControllersMediator mediator) : base(gameContext)
        {
            _context = gameContext;
            _runtimeData = runtimeData;
            _mediator = mediator;
            _entities = _context.GetGroup(GameMatcher.Team);
            _movableEntities = _context.GetGroup(GameMatcher.Movable);
        }

        public void Initialize()
        {
            _activeTeams = _entities.GetEntities(_buffer).Select(e => e.team.Type).Distinct().ToList();
            _context.SetControllable(_buffer.First());
            ChangeMovableTeam(_runtimeData.CurrentTeamMove);
            CheckWinner();
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.Movable.Removed());

        protected override bool Filter(GameEntity entity) => true;

        protected override void Execute(List<GameEntity> entities)
        {
            GameEntity firstMovable = FirstMovable(_currentTeam);
            if (firstMovable != null)
            {
                if (_context.controllable.Entity == entities[0])
                    firstMovable.tryControl = true;
            }
            else
            {
                ChangeMovableTeam(_activeTeams.GetNext(_currentTeam));
                CheckWinner();
            }
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
                    _context.SetTimer(entity, GameComponentsLookup.Movable, _runtimeData.MoveTime);
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
            if (Mathf.Abs(tank.rotation.eulerAngles.x) > c_maxAngle ||
                Mathf.Abs(tank.rotation.eulerAngles.z) > c_maxAngle)
                tank.SetPositionAndRotation(tank.position += Vector3.up, Quaternion.identity);
        }

        private void CheckWinner()
        {
            if (_activeTeams.Count > 1)
                _mediator.ChangeTeam(_currentTeam);
            else
            {
                _mediator.SetWinner(_currentTeam);
                _context.ReplaceWinnersTeam(_currentTeam);
            }
        }
    }
}