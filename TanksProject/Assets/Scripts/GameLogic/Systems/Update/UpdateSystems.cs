using General.Services;
using Tanks.Data;

namespace Tanks.GameLogic.Systems.Update
{
    internal sealed class UpdateSystems : Feature
    {
        public UpdateSystems(Contexts contexts, RuntimeData runtimeData, IInputService inputService,
            ITimeService timeService)
        {
            Add(new UpdateTimeSystem(contexts, timeService));
            Add(new InputSystems(contexts, runtimeData, inputService));
            Add(new TeamMoveChangeSystem(contexts, runtimeData));
            Add(new WeaponSystems(contexts));
            Add(new ControllableUpdateSystem(contexts, runtimeData));
            Add(new DamageImplementSystem(contexts));
            Add(new HealthControlSystem(contexts));
            Add(new ViewDeadActivateSystem(contexts));

            Add(new DestroySystem(contexts));
        }
    }
}