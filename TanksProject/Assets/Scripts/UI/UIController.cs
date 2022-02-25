using System;
using System.Threading.Tasks;
using General.Controllers;
using General.Services;
using UnityEngine;

namespace Tanks.UI
{
    public sealed class UIController : IUIController, IStart, IUpdate
    {
        public event Action OnMessageHideEvent;
        private readonly ITimeService _timeService;

        public UIController(ITimeService timeService)
        {
            _timeService = timeService;
        }
        
        public void Start()
        {
        }

        public void Update()
        {
        }

        public async void ShowMessage(string team, float moveTime)
        {
            await IntroduceTeam(team, moveTime);
            HideMessage();
        }

        private void HideMessage() => OnMessageHideEvent?.Invoke();

        private async Task IntroduceTeam(string team, float moveTime)
        {
            Debug.Log($"The {team} team`s turn for {moveTime} seconds");
            await Task.Delay(2000);
            Debug.Log("START!");
        }
    }
}
