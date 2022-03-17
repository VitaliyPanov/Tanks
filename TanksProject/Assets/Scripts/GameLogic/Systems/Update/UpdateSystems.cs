using Tanks.Data;
using Tanks.General.Controllers;
using Tanks.General.Services;

namespace Tanks.GameLogic.Systems.Update
{
    public sealed class UpdateSystems : Feature
    {
        public UpdateSystems(Contexts contexts, RuntimeData runtimeData, SceneStaticData staticData,
            ITimeService timeService, IPoolService poolService, IControllersMediator mediator)
        {
            Add(new UpdateTimeSystem(contexts.input, timeService));
            Add(new InputSystems(contexts.game, contexts.input));
            Add(new TeamMoveChangeSystem(contexts.game, runtimeData, mediator));
            Add(new WeaponSystems(contexts.game, contexts.input, poolService));
            Add(new ControllableUpdateSystem(contexts.game, mediator));
            Add(new HealthControlSystem(contexts.game));
            Add(new ViewDeadActivateSystem(contexts.game, staticData, poolService, mediator));

            Add(new DestroySystem(contexts.game));
        }
    }
}