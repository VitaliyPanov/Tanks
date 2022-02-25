//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Tanks.GameLogic.Components.Game.Weapon.WeaponLaunchTimeComponent weaponLaunchTime { get { return (Tanks.GameLogic.Components.Game.Weapon.WeaponLaunchTimeComponent)GetComponent(GameComponentsLookup.WeaponLaunchTime); } }
    public bool hasWeaponLaunchTime { get { return HasComponent(GameComponentsLookup.WeaponLaunchTime); } }

    public void AddWeaponLaunchTime(float newValue) {
        var index = GameComponentsLookup.WeaponLaunchTime;
        var component = (Tanks.GameLogic.Components.Game.Weapon.WeaponLaunchTimeComponent)CreateComponent(index, typeof(Tanks.GameLogic.Components.Game.Weapon.WeaponLaunchTimeComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceWeaponLaunchTime(float newValue) {
        var index = GameComponentsLookup.WeaponLaunchTime;
        var component = (Tanks.GameLogic.Components.Game.Weapon.WeaponLaunchTimeComponent)CreateComponent(index, typeof(Tanks.GameLogic.Components.Game.Weapon.WeaponLaunchTimeComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveWeaponLaunchTime() {
        RemoveComponent(GameComponentsLookup.WeaponLaunchTime);
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

    static Entitas.IMatcher<GameEntity> _matcherWeaponLaunchTime;

    public static Entitas.IMatcher<GameEntity> WeaponLaunchTime {
        get {
            if (_matcherWeaponLaunchTime == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.WeaponLaunchTime);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherWeaponLaunchTime = matcher;
            }

            return _matcherWeaponLaunchTime;
        }
    }
}
