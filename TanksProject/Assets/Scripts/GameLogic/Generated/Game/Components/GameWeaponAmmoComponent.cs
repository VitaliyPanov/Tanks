//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public TanksGB.GameLogic.Components.Game.Weapon.WeaponAmmoComponent weaponAmmo { get { return (TanksGB.GameLogic.Components.Game.Weapon.WeaponAmmoComponent)GetComponent(GameComponentsLookup.WeaponAmmo); } }
    public bool hasWeaponAmmo { get { return HasComponent(GameComponentsLookup.WeaponAmmo); } }

    public void AddWeaponAmmo(TanksGB.Data.AmmoData newData) {
        var index = GameComponentsLookup.WeaponAmmo;
        var component = (TanksGB.GameLogic.Components.Game.Weapon.WeaponAmmoComponent)CreateComponent(index, typeof(TanksGB.GameLogic.Components.Game.Weapon.WeaponAmmoComponent));
        component.Data = newData;
        AddComponent(index, component);
    }

    public void ReplaceWeaponAmmo(TanksGB.Data.AmmoData newData) {
        var index = GameComponentsLookup.WeaponAmmo;
        var component = (TanksGB.GameLogic.Components.Game.Weapon.WeaponAmmoComponent)CreateComponent(index, typeof(TanksGB.GameLogic.Components.Game.Weapon.WeaponAmmoComponent));
        component.Data = newData;
        ReplaceComponent(index, component);
    }

    public void RemoveWeaponAmmo() {
        RemoveComponent(GameComponentsLookup.WeaponAmmo);
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentMatcherApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public sealed partial class GameMatcher {

    static Entitas.IMatcher<GameEntity> _matcherWeaponAmmo;

    public static Entitas.IMatcher<GameEntity> WeaponAmmo {
        get {
            if (_matcherWeaponAmmo == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.WeaponAmmo);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherWeaponAmmo = matcher;
            }

            return _matcherWeaponAmmo;
        }
    }
}
