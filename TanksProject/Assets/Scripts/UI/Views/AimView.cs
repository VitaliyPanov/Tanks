using Tanks.General.UI.ViewModels;
using UnityEngine;
using UnityEngine.UI;

namespace Tanks.UI.Views
{
    public class AimView : MonoBehaviour
    {
        private IWeaponViewModel _viewModel;
        private Slider _weaponSlider;
        private bool _isActive;
        private float _launchingSpeed;

        private void Awake() => _weaponSlider = GetComponentInChildren<Slider>();

        private void Start()
        {
            gameObject.SetActive(false);
        }

        private void Update()
        {
            if (!_isActive) return;
            _weaponSlider.value += _launchingSpeed * Time.deltaTime;
        }

        public void Construct(IWeaponViewModel viewModel, Vector3 viewLocalPosition)
        {
            transform.localPosition = viewLocalPosition;
            _viewModel = viewModel;
            _viewModel.OnWeaponShellActivateEvent += ToggleArrow;
        }

        private void ToggleArrow(bool isActive, float maxLaunchingTime)
        {
            if (maxLaunchingTime != 0)
                _launchingSpeed = 1 / maxLaunchingTime;
            gameObject.SetActive(isActive);
            _weaponSlider.value = 0;
            _isActive = isActive;
        }

        private void OnDestroy() => _viewModel.OnWeaponShellActivateEvent -= ToggleArrow;
    }
}