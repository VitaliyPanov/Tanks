using System.Threading.Tasks;
using Tanks.Data;
using Tanks.GameLogic.Views.Behaviours;
using Tanks.General.Controllers;
using Tanks.General.Services;
using UnityEngine;
using UnityEngine.UI;

namespace Tanks.UI
{
    public sealed class UIController : IUIController, IStart
    {
        private Interface _interface;
        private readonly IControllersMediator _mediator;
        private readonly ITimeService _timeService;
        private readonly IPoolService _poolService;
        private UIData _uiData;
        private float _maxLaunchingTime;
        private Slider _slider;

        public UIController(IControllersMediator mediator, ITimeService timeService, IPoolService poolService)
        {
            _mediator = mediator;
            _timeService = timeService;
            _poolService = poolService;
        }

        public void Initialize(UIData uiData)
        {
            _uiData = uiData;
            _interface = Object.Instantiate(uiData.InterfacePrefab).AddComponent<Interface>();
        }

        public void Start()
        {
            InstantiateHealthBars();
            InstantiateWeaponArrows();
        }

        private void InstantiateWeaponArrows()
        {
            WeaponBehaviour[] weaponHandlers = Object.FindObjectsOfType<WeaponBehaviour>();
            foreach (var weaponHandler in weaponHandlers)
            {
                _poolService.Instantiate<UIAimController>(_uiData.AimCanvas, weaponHandler.transform)
                    .Construct(weaponHandler);
            }
        }

        private void InstantiateHealthBars()
        {
            HealthBehaviour[] healthHandlers = Object.FindObjectsOfType<HealthBehaviour>();
            foreach (var healthHandler in healthHandlers)
            {
                _poolService.Instantiate<UIHealthController>(_uiData.HealthAimCanvas, healthHandler.transform)
                    .Construct(healthHandler);
            }
        }

        public async Task ShowMessage(string team, float moveTime) => await _interface.ShowMessage(team, moveTime);
    }
}