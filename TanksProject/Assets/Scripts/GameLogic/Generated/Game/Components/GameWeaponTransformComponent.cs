//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public TanksGB.GameLogic.Components.Game.Weapon.WeaponTransformComponent weaponTransform { get { return (TanksGB.GameLogic.Components.Game.Weapon.WeaponTransformComponent)GetComponent(GameComponentsLookup.WeaponTransform); } }
    public bool hasWeaponTransform { get { return HasComponent(GameComponentsLookup.WeaponTransform); } }

    public void AddWeaponTransform(UnityEngine.Transform newValue) {
        var index = GameComponentsLookup.WeaponTransform;
        var component = (TanksGB.GameLogic.Components.Game.Weapon.WeaponTransformComponent)CreateComponent(index, typeof(TanksGB.GameLogic.Components.Game.Weapon.WeaponTransformComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceWeaponTransform(UnityEngine.Transform newValue) {
        var index = GameComponentsLookup.WeaponTransform;
        var component = (TanksGB.GameLogic.Components.Game.Weapon.WeaponTransformComponent)CreateComponent(index, typeof(TanksGB.GameLogic.Components.Game.Weapon.WeaponTransformComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveWeaponTransform() {
        RemoveComponent(GameComponentsLookup.WeaponTransform);
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

    static Entitas.IMatcher<GameEntity> _matcherWeaponTransform;

    public static Entitas.IMatcher<GameEntity> WeaponTransform {
        get {
            if (_matcherWeaponTransform == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.WeaponTransform);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherWeaponTransform = matcher;
            }

            return _matcherWeaponTransform;
        }
    }
}
