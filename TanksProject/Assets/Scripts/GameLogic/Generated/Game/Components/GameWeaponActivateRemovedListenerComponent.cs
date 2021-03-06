//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public WeaponActivateRemovedListenerComponent weaponActivateRemovedListener { get { return (WeaponActivateRemovedListenerComponent)GetComponent(GameComponentsLookup.WeaponActivateRemovedListener); } }
    public bool hasWeaponActivateRemovedListener { get { return HasComponent(GameComponentsLookup.WeaponActivateRemovedListener); } }

    public void AddWeaponActivateRemovedListener(System.Collections.Generic.List<IWeaponActivateRemovedListener> newValue) {
        var index = GameComponentsLookup.WeaponActivateRemovedListener;
        var component = (WeaponActivateRemovedListenerComponent)CreateComponent(index, typeof(WeaponActivateRemovedListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceWeaponActivateRemovedListener(System.Collections.Generic.List<IWeaponActivateRemovedListener> newValue) {
        var index = GameComponentsLookup.WeaponActivateRemovedListener;
        var component = (WeaponActivateRemovedListenerComponent)CreateComponent(index, typeof(WeaponActivateRemovedListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveWeaponActivateRemovedListener() {
        RemoveComponent(GameComponentsLookup.WeaponActivateRemovedListener);
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

    static Entitas.IMatcher<GameEntity> _matcherWeaponActivateRemovedListener;

    public static Entitas.IMatcher<GameEntity> WeaponActivateRemovedListener {
        get {
            if (_matcherWeaponActivateRemovedListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.WeaponActivateRemovedListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherWeaponActivateRemovedListener = matcher;
            }

            return _matcherWeaponActivateRemovedListener;
        }
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.EventEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public void AddWeaponActivateRemovedListener(IWeaponActivateRemovedListener value) {
        var listeners = hasWeaponActivateRemovedListener
            ? weaponActivateRemovedListener.value
            : new System.Collections.Generic.List<IWeaponActivateRemovedListener>();
        listeners.Add(value);
        ReplaceWeaponActivateRemovedListener(listeners);
    }

    public void RemoveWeaponActivateRemovedListener(IWeaponActivateRemovedListener value, bool removeComponentWhenEmpty = true) {
        var listeners = weaponActivateRemovedListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveWeaponActivateRemovedListener();
        } else {
            ReplaceWeaponActivateRemovedListener(listeners);
        }
    }
}
