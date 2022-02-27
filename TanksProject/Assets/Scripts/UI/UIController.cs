using System;
using System.Threading.Tasks;
using General.Controllers;
using General.Services;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace Tanks.UI
{
    public sealed class UIController : IUIController, IStart
    {
        private const string c_infoLabel = "InfoLabel";
        private const string c_roundLabel = "RoundLabel";
        private const string c_movementInfo = "Movement - WASD\nAttack - SPACE\nToggle tanks - Arrows(L-R)";
        private VisualElement _root;
        public event Action OnMessageHideEvent;
        private readonly ITimeService _timeService;
        private Label _roundLabel;

        public UIController(ITimeService timeService)
        {
            _timeService = timeService;
        }

        public void Initialize(GameObject prefab)
        {
            _root = Object.Instantiate(prefab).GetComponent<UIDocument>().rootVisualElement;
            _roundLabel = _root.Q<Label>(c_roundLabel);
            _roundLabel.text = "";
        }

        public void Start()
        {
            Label infoLabel = _root.Q<Label>(c_infoLabel);
            infoLabel.text = c_movementInfo;
            HideInfo(infoLabel);
        }
        
        public async void ShowMessage(string team, float moveTime)
        {
            await IntroduceTeam(team, moveTime);
            HideMessage();
        }

        private void HideMessage() => OnMessageHideEvent?.Invoke();

        private async Task IntroduceTeam(string team, float moveTime)
        {
            _roundLabel.text = $"The {team} team`s turn for {moveTime} seconds";
            await Task.Delay(2000);
            _roundLabel.text = "";
        }

        private async void HideInfo(Label info)
        {
            await Task.Delay(5000);
            info.text = null;
        }
    }
}
