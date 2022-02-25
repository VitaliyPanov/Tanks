using Entitas;
using General.Services;

namespace Tanks.GameLogic.Systems.Update
{
    internal sealed class UpdateTimeSystem : IExecuteSystem, IInitializeSystem
    {
        private readonly InputContext _context;
        private readonly ITimeService _timeService;

        public UpdateTimeSystem(Contexts contexts, ITimeService timeService)
        {
            _context = contexts.input;
            _timeService = timeService;
        }

        public void Initialize()
        {
            Execute();
        }

        public void Execute()
        {
            if (_context.isPause)
            {
                _context.ReplaceFixedDeltaTime(0);
                _context.ReplaceDeltaTime(0);
            }
            else
            {
                _context.ReplaceFixedDeltaTime(_timeService.FixedDeltaTime());
                _context.ReplaceDeltaTime(_timeService.DeltaTime());
            }
            _context.ReplaceRealtimeSinceStartup(_timeService.RealtimeSinceStartup());
        }
    }
}