using System;
using Entitas;
using Tanks.Data;
using Tanks.GameLogic.Services;
using Tanks.General.UI.ViewModels;
using UnityEngine;

namespace Tanks.GameLogic.Views.Behaviours
{
    [RequireComponent(typeof(UnityView))]
    public class WeaponBehaviour : MonoBehaviour, IBehaviour, IEventListener, IWeaponLaunchingListener,
        IWeaponActivateRemovedListener
    {
        private GameEntity _gameEntity;
        private IWeaponViewModel _viewModel;


        public void Construct(IEntity entity)
        {
            if (entity is not GameEntity gameEntity) return;
            _gameEntity = gameEntity;
        }

        public void Construct(IWeaponViewModel viewModel) => _viewModel ??= viewModel;

        public void AddListener(IEntity entity)
        {
            _gameEntity.AddWeaponLaunchingListener(this);
            _gameEntity.AddWeaponActivateRemovedListener(this);
        }

        public void OnWeaponLaunching(GameEntity entity)
        {
            if (_gameEntity.weaponAmmo.Data.Type == AmmoType.Shell)
                _viewModel.ToggleShellAim(true);
        }

        public void OnWeaponActivateRemoved(GameEntity entity)
        {
            _gameEntity.isWeaponLaunching = false;
            _viewModel.ToggleShellAim(false);
        }
    }
}