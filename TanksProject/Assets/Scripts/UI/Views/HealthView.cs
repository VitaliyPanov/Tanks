using System;
using Tanks.General.UI.ViewModels;
using UnityEngine;
using UnityEngine.UI;

namespace Tanks.UI.Views
{
    public sealed class HealthView : MonoBehaviour
    {
        private const string c_fillImage = "Fill Image";
        private readonly Color _zeroHealthColor = Color.red;
        private readonly Color _fullHealthColor = Color.green;
        private IHealthViewModel _viewModel;
        private Slider _healthSlider;
        private Image _healthImage;
        private float _startRotationX;
        private Transform _parentTransform;

        public void Construct(IHealthViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.OnHealthChangedEvent += ChangeSlider;
        }

        private void Awake()
        {
            _startRotationX = transform.localRotation.eulerAngles.x;
            _parentTransform = transform.parent;
            _healthSlider = GetComponentInChildren<Slider>();
            Image[] images = GetComponentsInChildren<Image>();
            foreach (var image in images)
            {
                if (image.name != c_fillImage) continue;
                _healthImage = image;
            }

            if (_healthImage == null || _healthSlider == null)
                throw new ArgumentException($"Can`t find components on {gameObject}");
        }

        private void Start() => ChangeSlider(_viewModel.HealthModel.CurrentHealth, _viewModel.HealthModel.MaxHealth);

        private void LateUpdate()
        {
            Quaternion rotation = Quaternion.Euler(_startRotationX - _parentTransform.rotation.eulerAngles.x, 0, 0);
            transform.rotation = rotation;
        }

        private void ChangeSlider(float current, float max)
        {
            _healthSlider.value = current / max;
            _healthImage.color = Color.Lerp(_zeroHealthColor, _fullHealthColor, current / max);
        }

        private void OnDestroy() => _viewModel.OnHealthChangedEvent -= ChangeSlider;
    }
}