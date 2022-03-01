using System;
using Tanks.GameLogic.Views.Behaviours;
using UnityEngine;
using UnityEngine.UI;

namespace Tanks.UI
{
    public sealed class UIHealthController : MonoBehaviour
    {
        private const string c_fillImage = "Fill Image";
        private Slider _healthSlider;
        private Image _healthImage;
        private Color _fullHealthColor = Color.green;
        private Color _zeroHealthColor = Color.red;
        
        private HealthBehaviour _handler;

        private void Awake()
        {
            _healthSlider = GetComponentInChildren<Slider>();
            Image[] images = GetComponentsInChildren<Image>();
            foreach (var image in images)
            {
                if (image.name != c_fillImage) continue;
                _healthImage = image;
            }
            if (_healthImage == null || _healthSlider == null) throw new ArgumentException($"Can`t find components on {gameObject}");
        }

        private void Start()
        {
            ChangeSlider(_handler.CurrentHealth, _handler.MaxHealth);
        }

        public void Construct(HealthBehaviour handler)
        {
            _handler = handler;
            _handler.OnHealthChangedEvent += ChangeSlider;
        }

        private void ChangeSlider(float current, float max)
        {
            _healthSlider.value = current / max;
            _healthImage.color = Color.Lerp(_zeroHealthColor, _fullHealthColor, current / max);
        }

        private void OnDestroy() => _handler.OnHealthChangedEvent -= ChangeSlider;
    }
}