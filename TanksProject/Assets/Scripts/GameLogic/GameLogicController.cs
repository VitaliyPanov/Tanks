using General.Controllers;
using General.Services;
using TanksGB.Data;
using TanksGB.GameLogic.Services.View;
using TanksGB.GameLogic.Systems.FixedUpdate;
using TanksGB.GameLogic.Systems.Init;
using TanksGB.GameLogic.Systems.Update;

namespace TanksGB.GameLogic
{
    public class GameLogicController : ILogicController, IStart, IUpdate, IFixedUpdate
    {
        private readonly IInputService _inputService;
        private readonly IDataService _dataService;
        private readonly ITimeService _timeService;
        private readonly IPoolService _poolService;
        private InitSystems _initSystems;
        private UpdateSystems _updateSystems;
        private FixedUpdateSystems _fixedUpdateSystems;
        private Contexts _contexts;

        public GameLogicController(IPoolService poolService, IInputService inputService, IDataService dataService,
            ITimeService timeService)
        {
            _inputService = inputService;
            _dataService = dataService;
            _timeService = timeService;
            _poolService = poolService;
        }

        public void Initialize(string sceneName)
        {
            SceneStaticData staticData = _dataService.StaticData(sceneName);
            RuntimeData runtimeData = _dataService.RuntimeData;
            runtimeData.ChangeTeam(staticData.FirstMoveTeam);
            
            _contexts = Contexts.sharedInstance;
            BindLocalServices();
            CreateSystems(staticData, runtimeData);
        }

        public void Start()
        {
            _initSystems.Initialize();
            _updateSystems.Initialize();
            _fixedUpdateSystems.Initialize();
        }

        public void Update()
        {
            _updateSystems.Execute();
            _updateSystems.Cleanup();
        }

        public void FixedUpdate() => _fixedUpdateSystems.Execute();

        private void BindLocalServices() => _contexts.game.SetViewService(new ViewService(_poolService));

        private void CreateSystems(SceneStaticData staticData, RuntimeData runtimeData)
        {
            _initSystems = new InitSystems(_contexts, staticData, runtimeData);
            _updateSystems = new UpdateSystems(_contexts, runtimeData, _inputService, _timeService);
            _fixedUpdateSystems = new FixedUpdateSystems(_contexts, runtimeData);
        }

        public void Pause(bool isPause)
        {
            _updateSystems.paused = isPause;
            _fixedUpdateSystems.paused = isPause;
        }
    }
}