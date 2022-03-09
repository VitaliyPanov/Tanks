using Tanks.Data;
using Tanks.GameLogic.Services.View;
using Tanks.GameLogic.Systems.AI;
using Tanks.GameLogic.Systems.FixedUpdate;
using Tanks.GameLogic.Systems.Init;
using Tanks.GameLogic.Systems.Update;
using Tanks.General.Controllers;
using Tanks.General.Services;

namespace Tanks.GameLogic
{
    public class GameLogicController : ILogicController, IStart, IUpdate, IFixedUpdate
    {
        private readonly IInputService _inputService;
        private readonly IDataService _dataService;
        private readonly ITimeService _timeService;
        private readonly IControllersMediator _mediator;
        private readonly IPoolService _poolService;
        private InitSystems _initSystems;
        private UpdateSystems _updateSystems;
        private FixedUpdateSystems _fixedUpdateSystems;
        private AISystems _aiSystems;
        private Contexts _contexts;

        public GameLogicController(IPoolService poolService, IInputService inputService, IDataService dataService,
            ITimeService timeService, IControllersMediator mediator)
        {
            _inputService = inputService;
            _dataService = dataService;
            _timeService = timeService;
            _mediator = mediator;
            _poolService = poolService;
        }

        public void Initialize(SceneStaticData staticData, RuntimeData runtimeData)
        {
            runtimeData.CurrentTeamMove = staticData.FirstMoveTeam;
            _contexts = Contexts.sharedInstance;

            BindLocalServices();
            CreateSystems(staticData, runtimeData);
        }

        public void Start()
        {
            _initSystems.Initialize();
            _updateSystems.Initialize();
            _fixedUpdateSystems.Initialize();
            _aiSystems.Initialize();
        }

        public void Update()
        {
            _updateSystems.Execute();
            _updateSystems.Cleanup();
        }

        public void FixedUpdate()
        {
            _fixedUpdateSystems.Execute();
            _fixedUpdateSystems.Cleanup();
            _aiSystems.Execute();
            _aiSystems.Cleanup();
        }

        private void BindLocalServices() => _contexts.game.SetViewService(new ViewService(_poolService));

        private void CreateSystems(SceneStaticData staticData, RuntimeData runtimeData)
        {
            _initSystems = new InitSystems(_contexts, staticData, runtimeData);
            _updateSystems = new UpdateSystems(_contexts, runtimeData, staticData, _inputService, _timeService,_poolService, _mediator);
            _fixedUpdateSystems = new FixedUpdateSystems(_contexts, runtimeData, _poolService);
            _aiSystems = new AISystems(_contexts, _dataService);
        }

        public void Pause(bool isPause) => _contexts.input.isPause = isPause;
    }
}