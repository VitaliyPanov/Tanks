using UnityEngine;
using UnityEngine.UIElements;

namespace General.UI
{
    public sealed class StartMenu:MonoBehaviour
    {
        [SerializeField] private GameObject _bootstrapperPrefab;
        private const string c_startBtn = "start_btn";

        private VisualElement _root;
        private Button _startButton;

        private void Awake()
        {
            _root = GetComponent<UIDocument>().rootVisualElement;
            _startButton = _root.Q<Button>(c_startBtn);
            _startButton.clicked += OnStartButtonClicked;
        }

        private void OnStartButtonClicked()
        {
            Instantiate(_bootstrapperPrefab);
        }

        private void OnDestroy()
        {
            _startButton.clicked -= OnStartButtonClicked;
        }
    }
}