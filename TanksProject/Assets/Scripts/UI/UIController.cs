using System.Threading.Tasks;
using Tanks.Data;
using Tanks.GameLogic.Views.Behaviours;
using Tanks.General.Controllers;
using Tanks.General.Services;
using Tanks.General.Services.Input;
using Tanks.UI.Models;
using Tanks.UI.ViewModels;
using Tanks.UI.Views;
using UnityEngine;
using Slider = UnityEngine.UI.Slider;

namespace Tanks.UI
{
    public sealed class UIController : IUIController, IStart
    {
        private const string c_fireTransform = "FireTransform";
        private readonly IPoolService _poolService;
        private readonly IDataService _dataService;
        private readonly IInputService _inputService;
        private HUD _hud;
        private MiniMapController _miniMap;
        private UIData _uiData;
        private float _maxLaunchingTime;
        private Slider _slider;

        public UIController(IPoolService poolService, IDataService dataService, IInputService inputService)
        {
            _poolService = poolService;
            _dataService = dataService;
            _inputService = inputService;
        }

        public void Initialize(UIData uiData)
        {
            _uiData = uiData;
            _hud = Object.Instantiate(_uiData.InterfacePrefab).AddComponent<HUD>();
        }

        public void Start()
        {
            InstantiateHealthBars();
            InstantiateWeaponArrows();
            
            _miniMap = Object.Instantiate(_uiData.MinimapPrefab).AddComponent<MiniMapController>();
            _miniMap.Construct(_inputService);
        }

        private void InstantiateWeaponArrows()
        {
            float maxLaunchingTime = _dataService.AmmunitionData(AmmoType.Shell).MaxLaunchingTime;
            WeaponBehaviour[] weaponHandlers = Object.FindObjectsOfType<WeaponBehaviour>();
            foreach (var weaponHandler in weaponHandlers)
            {
                WeaponModel weaponModel = new WeaponModel(maxLaunchingTime);
                WeaponViewModel weaponViewModel = new WeaponViewModel(weaponModel);

                weaponHandler.Construct(weaponViewModel);
                _poolService.Instantiate<AimView>(_uiData.AimCanvas, weaponHandler.transform)
                    .Construct(weaponViewModel, weaponHandler.transform.Find(c_fireTransform).localPosition);
            }
        }

        private void InstantiateHealthBars()
        {
            HealthBehaviour[] healthHandlers = Object.FindObjectsOfType<HealthBehaviour>();
            foreach (var healthHandler in healthHandlers)
            {
                HealthModel healthModel = new HealthModel(healthHandler.MaxHealth, healthHandler.CurrentHealth);
                HealthViewModel viewModel = new HealthViewModel(healthModel);

                healthHandler.Construct(viewModel);
                _poolService.Instantiate<HealthView>(_uiData.HealthAimCanvas, healthHandler.transform)
                    .Construct(viewModel);
            }
        }

        public void SetPlayer(Transform target, string id) => _miniMap.TrySetPlayer(target, id);
        public void RemoveMapElement(string id) => _miniMap.TryRemoveElement(id);

        public async Task ShowTeamMove(TeamType team, float moveTime) =>
            await _hud.ShowMessage($"The {team} team`s turn for {moveTime} seconds");

        public async void ShowWinner(TeamType team) => await _hud.ShowMessage($"The {team} team is WINNER");
    }
}