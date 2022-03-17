using System.Collections.Generic;
using System.Linq;
using Tanks.GameLogic.Views;
using Tanks.General.Services.Input;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

namespace Tanks.UI
{
    public sealed class MiniMapController : MonoBehaviour, InputControls.IUIActions
    {
        private const string c_miniMap = "root-container-mini";
        private const string c_fullMap = "root-container-full";
        private const float c_miniMultiplier = 2.9f;
        private const float c_fullMultiplier = 7.5f;

        private VisualElement _root;
        private VisualElement _mapImage;
        private VisualElement _mapContainer;

        private Dictionary<string, MapElement> _elementsContainer;
        private Transform _currentPlayer;
        private MapElement _playerRepresentation;
        private bool _isMapOpen;

        public void Construct(IInputService inputService)
        {
            inputService.RegisterUIListener(this);
            _root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container");
            _mapImage = _root.Q<VisualElement>("Image");
            _mapContainer = _root.Q<VisualElement>("Map");

            UnityView[] views = FindObjectsOfType<UnityView>();
            _elementsContainer = new Dictionary<string, MapElement>(views.Length);
            CreateMapElements(views);
            _playerRepresentation = _elementsContainer.Values.First();
        }

        private void LateUpdate()
        {
            var multiplier = _isMapOpen ? c_fullMultiplier : c_miniMultiplier;
            SetElementPosition(_playerRepresentation, _currentPlayer, multiplier);
            if (!_isMapOpen)
            {
                var clampWidth = _mapImage.worldBound.width / 2 - _mapContainer.worldBound.width / 2;
                var clampHeight = _mapImage.worldBound.height / 2 - _mapContainer.worldBound.height / 2;
                var xPos = Mathf.Clamp(_currentPlayer.transform.position.x * -multiplier, -clampWidth, clampWidth);
                var yPos = Mathf.Clamp(_currentPlayer.transform.position.z * multiplier, -clampHeight, clampHeight);
                _mapImage.style.translate = new Translate(xPos, yPos, 0);
            }
            else
                _mapImage.style.translate = new Translate(0, 0, 0);
        }

        public void TrySetPlayer(Transform target, string id)
        {
            if (_elementsContainer.TryGetValue(id, out MapElement element))
            {
                _currentPlayer = target;
                _playerRepresentation = element;
            }
        }

        public void TryRemoveElement(string id)
        {
            if (_elementsContainer.Remove(id, out MapElement element))
                _mapImage.Remove(element);
        }

        private void OpenMap(bool open)
        {
            _isMapOpen = open;
            _root.EnableInClassList(c_miniMap, !open);
            _root.EnableInClassList(c_fullMap, open);
            ScaleCoordinates(open ? c_fullMultiplier / c_miniMultiplier : c_miniMultiplier / c_fullMultiplier);
        }

        private void ScaleCoordinates(float multiplier)
        {
            foreach (var visualElement in _elementsContainer.Values)
            {
                visualElement.style.translate = new Translate(visualElement.transform.position.x * multiplier,
                    visualElement.transform.position.y * multiplier, 0);
            }
        }

        private void CreateMapElements(IEnumerable<UnityView> views)
        {
            var multiplier = _isMapOpen ? c_fullMultiplier : c_miniMultiplier;
            foreach (var view in views)
            {
                var viewElement = CreateElement(view);
                _mapImage.Add(viewElement);
                _elementsContainer[view.UniqID] = viewElement;
                SetElementPosition(viewElement, view.transform, multiplier);
            }
        }

        private static void SetElementPosition(MapElement element, Transform transform, float multiplier)
        {
            element.style.translate = new Translate(transform.position.x * multiplier, transform.position.z * -multiplier, 0);
            element.style.rotate = new Rotate(new Angle(transform.rotation.eulerAngles.y));
        }

        private MapElement CreateElement(IView view) => 
            view.GameObject.TryGetComponent(out TeamBehaviour team)
            ? new TeamMapElement(team.Team)
            : new MapElement();

        public void OnToggleMinimap(InputAction.CallbackContext context)
        {
            if (context.started)
                OpenMap(true);
            else if (context.canceled)
                OpenMap(false);
        }
    }
}