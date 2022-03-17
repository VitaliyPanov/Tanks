using System;
using System.Collections.Generic;
using Entitas;
using Tanks.Data;
using Tanks.GameLogic.Services;
using Tanks.General.Services;
using UnityEngine;

namespace Tanks.GameLogic.Systems.Weapon
{
    internal sealed class WeaponShootSystem : ReactiveSystem<GameEntity>, ICleanupSystem
    {
        private readonly IPoolService _poolService;
        private readonly float movableTimeAfterFire = 3f;
        private readonly GameContext _context;
        private readonly IGroup<GameEntity> _shootStopGroup;
        private List<GameEntity> _buffer = new();

        public WeaponShootSystem(GameContext gameContext, IPoolService poolService) : base(gameContext)
        {
            _poolService = poolService;
            _context = gameContext;
            _shootStopGroup = gameContext.GetGroup(GameMatcher
                    .AllOf(GameMatcher.WeaponActivate)
                    .NoneOf(GameMatcher.Control));
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
                        var force = CalculateLaunchingForce(ammoData, entity.weaponLaunchTime.Value);
                        bulletEntity.AddShell(ammoData.ExplosionForce, ammoData.ExplosionRadius);
                        InstantiateWithForce(ammoData, bulletEntity, fireTransform.forward, force);
                        break;
                    case AmmoType.Bullet:
                        Debug.Log("Tra-ta-ta-ta-ta-ta");
                        break;
                    default: throw new ArgumentOutOfRangeException();
                }
                
                ChangeMovable(entity);
                CooldownWeapon(entity);
            }
        }

        private void ChangeMovable(GameEntity entity)
        {
            entity.isWeaponFired = true;
            _context.SetTimer(entity, GameComponentsLookup.Movable, movableTimeAfterFire);
        }

        private GameEntity CreateBulletEntity(Transform fireTransform, float damage)
        {
            GameEntity bulletEntity = _context.CreateEntity();
            bulletEntity.AddPosition(fireTransform.position);
            bulletEntity.AddRotation(fireTransform.rotation);
            bulletEntity.AddDamage(damage);
            return bulletEntity;
        }

        private void InstantiateWithForce(AmmoData ammoData, GameEntity bulletEntity, Vector3 direction, float force)
        {
            Rigidbody shellBody = _context.viewService.value.CreateImmediately(ammoData.Prefab, bulletEntity)
                .GameObject.GetOrAddComponent<Rigidbody>();

            ParticleSystem shellSteam = _poolService.Instantiate<ParticleSystem>(ammoData.Steam, shellBody.transform);
            shellSteam.Play();
            _context.CreateEntity().AddParticle(shellSteam);
            bulletEntity.AddShellSteam(shellSteam.transform);
            shellBody.velocity = direction * force;
        }

        private static float CalculateLaunchingForce(AmmoData ammoData, float launchTime) =>
            ammoData.MinLaunchForce + (ammoData.MaxLaunchForce - ammoData.MinLaunchForce) * launchTime /
            ammoData.MaxLaunchingTime;

        private void CooldownWeapon(GameEntity entity)
        {
            entity.isWeaponCooldown = true;
            _context.SetTimer(entity, GameComponentsLookup.WeaponCooldown, entity.weaponAmmo.Data.CooldownTime);
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