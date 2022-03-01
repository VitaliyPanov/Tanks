using System;
using Entitas;
using Tanks.Data;
using Tanks.GameLogic.Services;
using UnityEngine;

namespace Tanks.GameLogic.Views.Behaviours
{
    [RequireComponent(typeof(UnityView))]
    public class WeaponBehaviour : MonoBehaviour, IBehaviour, IEventListener, IWeaponLaunchingListener,
        IWeaponActivateRemovedListener
    {
        public event Action<bool, float> OnWeaponShellActivateEvent;
        private GameEntity _gameEntity;


        public void Initialize(IEntity entity)
        {
            if (entity is not GameEntity gameEntity) return;
            _gameEntity = gameEntity;
        }

        public void AddListener(IEntity entity)
        {
            _gameEntity.AddWeaponLaunchingListener(this);
            _gameEntity.AddWeaponActivateRemovedListener(this);
        }

        public void OnWeaponLaunching(GameEntity entity)
        {
            if (_gameEntity.weaponAmmo.Data.Type == AmmoType.Shell)
                OnWeaponShellActivateEvent?.Invoke(true, _gameEntity.weaponAmmo.Data.MaxLaunchingTime);
        }

        public void OnWeaponActivateRemoved(GameEntity entity)
        {
            _gameEntity.isWeaponLaunching = false;
            OnWeaponShellActivateEvent?.Invoke(false, 0);
        }
    }
}