using System.Collections.Generic;
using System.Linq;
using Tanks.GameLogic.Views;
using Tanks.General.Services.Input;
using UnityEngine;
using UnityEngine.UIElements;

namespace Tanks.UI
{
    public sealed class MiniMapController : MonoBehaviour
    {
        private const string c_miniMap = "root-container-mini";
        private const string c_fullMap = "root-container-full";
        private const float c_miniMultiplier = 2.9f;
        private const float c_fullMultiplier = 7.7f;

        private IInputService _inputService;
        private VisualElement _root;
        private VisualElement _mapImage;
        private VisualElement _playerRepresentation;
        private VisualElement _mapContainer;

        private Dictionary<string, MapElement> _existingElements;
        private Transform _currentPlayer;
        private bool _isMapOpen;

        public void Construct(IInputService inputService)
        {
            _inputService = inputService;
            _inputService.OnToggleMiniMapEvent += OpenMap;
            _root = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("Container");
            _mapImage = _root.Q<VisualElement>("Image");
            _mapContainer = _root.Q<VisualElement>("Map");

            UnityView[] views = FindObjectsOfType<UnityView>();
            _existingElements = new Dictionary<string, MapElement>(views.Length);
            CreateMapElements(views);
            _playerRepresentation = _existingElements.Values.First();
        }

        private void LateUpdate()
        {
            var multiplier = _isMapOpen ? c_fullMultiplier : c_miniMultiplier;
            _playerRepresentation.style.translate = new Translate(_currentPlayer.transform.position.x * multiplier,
                _currentPlayer.transform.position.z * -multiplier, 0);
            _playerRepresentation.style.rotate = new Rotate(new Angle(_currentPlayer.transform.rotation.eulerAngles.y));

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
            if (_existingElements.TryGetValue(id, out MapElement element))
            {
                _currentPlayer = target;
                _playerRepresentation = element;
            }
        }

        public void TryRemoveElement(string id)
        {
            if (_existingElements.Remove(id,out MapElement element))
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
            foreach (MapElement element in _existingElements.Values)
            {
                element.style.translate = new Translate(element.transform.position.x * multiplier,
                    element.transform.position.y * multiplier, 0);
            }
        }

        private void CreateMapElements(IEnumerable<UnityView> views)
        {
            var multiplier = _isMapOpen ? c_fullMultiplier : c_miniMultiplier;
            foreach (var view in views)
            {
                var viewElement = CreateElement(view);
                viewElement.style.translate = new Translate(view.transform.position.x * multiplier,
                    view.transform.position.z * -multiplier, 0);
                viewElement.style.rotate = new Rotate(new Angle(view.transform.rotation.eulerAngles.y));
            }
        }

        private MapElement CreateElement(IView view)
        {
            var viewElement = new MapElement();
            _mapImage.Add(viewElement);
            _existingElements[view.UniqID] = viewElement;
            return viewElement;
        }

        private void OnDisable() => _inputService.OnToggleMiniMapEvent -= OpenMap;
    }
}