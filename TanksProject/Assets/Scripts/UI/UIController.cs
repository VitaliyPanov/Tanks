using System.Threading.Tasks;
using Tanks.Data;
using Tanks.General.Controllers;
using Tanks.UI.UIControllers;
using UnityEngine;

namespace Tanks.UI
{
    public sealed class UIController : IUIController, IStart, IUpdate
    {
        private Controllers _controllers;
        private Interface _interface;
        private readonly IControllersMediator _mediator;
        private UIData _uiData;

        public UIController(IControllersMediator mediator) => _mediator = mediator;

        public void Initialize(UIData uiData)
        {
            _uiData = uiData;
            _controllers = new Controllers();
            _controllers.Add(new HealthAimController(_uiData.HealthAimCanvas));
            
            
            
            
            
            _interface = Object.Instantiate(uiData.InterfacePrefab).AddComponent<Interface>();
        }

        public void Start()
        {
            _controllers.Start();
        }

        public void Update()
        {
            _controllers.Update();
        }

        public async Task ShowMessage(string team, float moveTime) => await _interface.ShowMessage(team, moveTime);
    }
}