using Tanks.Data;
using Tanks.General.Controllers;
using Tanks.General.Services;
using Tanks.General.Services.Input;

namespace Tanks.GameLogic.Systems.Update
{
    public sealed class UpdateSystems : Feature
    {
        public UpdateSystems(Contexts contexts, RuntimeData runtimeData, SceneStaticData staticData,
            IInputService inputService, ITimeService timeService, IPoolService poolService, IControllersMediator mediator)
        {
            Add(new UpdateTimeSystem(contexts, timeService));
            Add(new InputSystems(contexts, runtimeData, inputService));
            Add(new TeamMoveChangeSystem(contexts, runtimeData, mediator));
            Add(new WeaponSystems(contexts, poolService));
            Add(new ControllableUpdateSystem(contexts, mediator));
            Add(new HealthControlSystem(contexts));
            Add(new ViewDeadActivateSystem(contexts, staticData, poolService, mediator));
            
            Add(new DestroySystem(contexts));
        }
    }
}