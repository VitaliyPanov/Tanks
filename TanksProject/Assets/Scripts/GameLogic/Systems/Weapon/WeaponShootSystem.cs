using System;
using System.Collections.Generic;
using Entitas;
using TanksGB.Data;
using TanksGB.GameLogic.Services;
using UnityEngine;

namespace TanksGB.GameLogic.Systems.Weapon
{
    internal sealed class WeaponShootSystem : ReactiveSystem<GameEntity>, ICleanupSystem
    {
        private readonly float movableTimeAfterFire = 3f;
        private readonly GameContext _context;
        private readonly IGroup<GameEntity> _shootStopGroup;
        private List<GameEntity> _buffer = new();

        public WeaponShootSystem(Contexts contexts) : base(contexts.game)
        {
            _context = contexts.game;
            _shootStopGroup =
                contexts.game.GetGroup(GameMatcher.AllOf(GameMatcher.WeaponActivate).NoneOf(GameMatcher.Control));
        }

        protected override ICollector<GameEntity> GetTrigger(IContext<GameEntity> context) =>
            context.CreateCollector(GameMatcher.WeaponLaunching.Removed());

        protected override bool Filter(GameEntity entity) => entity.hasTransform;

        protected override void Execute(List<GameEntity> entities)
        {
            foreach (var entity in entities)
            {
                Transform fireTransform = entity.weaponTransform.Value;
                AmmoData ammoData = entity.weaponAmmo.Data;
                var bulletEntity = CreateBulletEntity(fireTransform, ammoData.Damage);

                switch (ammoData.Type)
                {
                    case AmmoType.Shell:
                        bulletEntity.AddShell(ammoData.ExplosionForce, ammoData.ExplosionRadius);
                        var force = CalculateLaunchingForce(ammoData, entity.weaponLaunchTime.Value);
                        InstantiateWithForce(ammoData.Prefab, bulletEntity, fireTransform.forward, force);
                        break;
                    case AmmoType.Bullet:
                        Debug.Log("Tra-ta-ta-ta-ta-ta");
                        break;
                    default: throw new ArgumentOutOfRangeException();
                }

                entity.isWeaponFired = true;
                _context.SetTimer(movableTimeAfterFire, GameComponentsLookup.Movable, entity);
                CooldownWeapon(entity);
            }
        }

        private GameEntity CreateBulletEntity(Transform fireTransform, float damage)
        {
            GameEntity bulletEntity = _context.CreateEntity();
            bulletEntity.AddPosition(fireTransform.position);
            bulletEntity.AddRotation(fireTransform.rotation);
            bulletEntity.AddDamage(damage);
            return bulletEntity;
        }

        private void InstantiateWithForce(GameObject prefab, GameEntity bulletEntity, Vector3 direction, float force)
        {
            Rigidbody shellBody = _context.viewService.value.CreateView(prefab, bulletEntity)
                .GameObject.GetOrAddComponent<Rigidbody>();
            shellBody.velocity = direction * force;
        }

        private static float CalculateLaunchingForce(AmmoData ammoData, float launchTime) =>
            ammoData.MinLaunchForce + (ammoData.MaxLaunchForce - ammoData.MinLaunchForce) * launchTime /
            ammoData.MaxLaunchingTime;

        private void CooldownWeapon(GameEntity entity)
        {
            entity.isWeaponCooldown = true;
            _context.SetTimer(entity.weaponAmmo.Data.CooldownTime, GameComponentsLookup.WeaponCooldown, entity);
        }

        public void Cleanup()
        {
            foreach (var entity in _shootStopGroup.GetEntities(_buffer))
            {
                entity.isWeaponActivate = false;
            }
        }
    }
}