using Entitas;
using General.Services;

namespace TanksGB.GameLogic.Systems.Update
{
    internal sealed class UpdateTimeSystem : IExecuteSystem, IInitializeSystem
    {
        private readonly Contexts _contexts;
        private readonly ITimeService _timeService;

        public UpdateTimeSystem(Contexts contexts, ITimeService timeService)
        {
            _contexts = contexts;
            _timeService = timeService;
        }

        public void Initialize()
        {
            Execute();
        }

        public void Execute()
        {
            _contexts.input.ReplaceFixedDeltaTime(_timeService.FixedDeltaTime());
            _contexts.input.ReplaceDeltaTime(_timeService.DeltaTime());
            _contexts.input.ReplaceRealtimeSinceStartup(_timeService.RealtimeSinceStartup());
        }
    }
}