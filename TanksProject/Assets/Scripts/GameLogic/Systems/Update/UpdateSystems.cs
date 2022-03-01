using Tanks.Data;
using Tanks.General.Controllers;
using Tanks.General.Services;

namespace Tanks.GameLogic.Systems.Update
{
    internal sealed class UpdateSystems : Feature
    {
        public UpdateSystems(Contexts contexts, RuntimeData runtimeData, IInputService inputService,
            ITimeService timeService, IControllersMediator mediator)
        {
            Add(new UpdateTimeSystem(contexts, timeService));
            Add(new InputSystems(contexts, runtimeData, inputService));
            Add(new TeamMoveChangeSystem(contexts, runtimeData, mediator));
            Add(new WeaponSystems(contexts));
            Add(new ControllableUpdateSystem(contexts, mediator));
            Add(new HealthControlSystem(contexts));
            Add(new ViewDeadActivateSystem(contexts));
            

            Add(new DestroySystem(contexts));
        }
    }
}