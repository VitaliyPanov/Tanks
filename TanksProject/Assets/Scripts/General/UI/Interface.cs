using UnityEngine;
using UnityEngine.UIElements;

namespace General.UI
{
    internal sealed class Interface : MonoBehaviour
    {
        private VisualElement _root;
        private Label _infoLabel;

        private void Awake()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _infoLabel = _root.Q<Label>("InfoLabel");
            Debug.Log(_infoLabel);
        }

        private void Start()
        {
            _infoLabel.text = "_________\n________";
        }
    }
}