using System.Threading.Tasks;
using Tanks.Data;
using UnityEngine;
using UnityEngine.UIElements;

namespace Tanks.UI
{
    internal sealed class HUD : MonoBehaviour
    {
        private VisualElement _root;
        private Label _roundLabel;

        private const string c_infoLabel = "InfoLabel";
        private const string c_roundLabel = "RoundLabel";
        private const string c_movementInfo = "Movement - WASD\nAttack - SPACE\nToggle tanks - Arrows(L-R)";
        
        private void Awake()
        {
            _root = gameObject.GetComponent<UIDocument>().rootVisualElement;
            _roundLabel = _root.Q<Label>(c_roundLabel);
            _roundLabel.text = "";
            DontDestroyOnLoad(this);
        }

        private void Start()
        {
            Label infoLabel = _root.Q<Label>(c_infoLabel);
            infoLabel.text = c_movementInfo;
            HideLabel(infoLabel);
        }
        
        public async Task ShowMessage(string message)
        {
            ToggleVisibility(_roundLabel, true);
            _roundLabel.text = message;
            await Task.Delay(3000);
            ToggleVisibility(_roundLabel, false);
        }

        private async void HideLabel(Label label)
        {
            await Task.Delay(5000);
            ToggleVisibility(label, false);
        }

        private void ToggleVisibility(Label label, bool isVisible)
        {
            label.SetEnabled(isVisible);
            label.visible = isVisible;
        }
    }
}