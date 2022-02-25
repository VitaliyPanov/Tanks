//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    static readonly Tanks.GameLogic.Components.Game.Weapon.WeaponActivateComponent weaponActivateComponent = new Tanks.GameLogic.Components.Game.Weapon.WeaponActivateComponent();

    public bool isWeaponActivate {
        get { return HasComponent(GameComponentsLookup.WeaponActivate); }
        set {
            if (value != isWeaponActivate) {
                var index = GameComponentsLookup.WeaponActivate;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : weaponActivateComponent;

                    AddComponent(index, component);
                } else {
                    RemoveComponent(index);
                }
            }
        }
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

    static Entitas.IMatcher<GameEntity> _matcherWeaponActivate;

    public static Entitas.IMatcher<GameEntity> WeaponActivate {
        get {
            if (_matcherWeaponActivate == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.WeaponActivate);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherWeaponActivate = matcher;
            }

            return _matcherWeaponActivate;
        }
    }
}
