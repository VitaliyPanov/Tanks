using System;
using Tanks.GameLogic.Views.Behaviours;
using UnityEngine;
using UnityEngine.UI;

namespace Tanks.UI
{
    public class UIAimController:MonoBehaviour
    {
        private const string c_fireTransform = "FireTransform";
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

        public void Construct(WeaponBehaviour weaponHandler)
        {
            transform.localPosition = weaponHandler.transform.Find(c_fireTransform).localPosition;
            weaponHandler.OnWeaponShellActivateEvent += ToggleArrow;
        }

        private void ToggleArrow(bool isActive, float maxLaunchingTime)
        {
            if (maxLaunchingTime != 0)
                _launchingSpeed = 1 / maxLaunchingTime;
            gameObject.SetActive(isActive);
            _weaponSlider.value = 0;
            _isActive = isActive;
        }
    }
}