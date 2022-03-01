//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public HealthDamageListenerComponent healthDamageListener { get { return (HealthDamageListenerComponent)GetComponent(GameComponentsLookup.HealthDamageListener); } }
    public bool hasHealthDamageListener { get { return HasComponent(GameComponentsLookup.HealthDamageListener); } }

    public void AddHealthDamageListener(System.Collections.Generic.List<IHealthDamageListener> newValue) {
        var index = GameComponentsLookup.HealthDamageListener;
        var component = (HealthDamageListenerComponent)CreateComponent(index, typeof(HealthDamageListenerComponent));
        component.value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceHealthDamageListener(System.Collections.Generic.List<IHealthDamageListener> newValue) {
        var index = GameComponentsLookup.HealthDamageListener;
        var component = (HealthDamageListenerComponent)CreateComponent(index, typeof(HealthDamageListenerComponent));
        component.value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveHealthDamageListener() {
        RemoveComponent(GameComponentsLookup.HealthDamageListener);
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

    static Entitas.IMatcher<GameEntity> _matcherHealthDamageListener;

    public static Entitas.IMatcher<GameEntity> HealthDamageListener {
        get {
            if (_matcherHealthDamageListener == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.HealthDamageListener);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherHealthDamageListener = matcher;
            }

            return _matcherHealthDamageListener;
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

    public void AddHealthDamageListener(IHealthDamageListener value) {
        var listeners = hasHealthDamageListener
            ? healthDamageListener.value
            : new System.Collections.Generic.List<IHealthDamageListener>();
        listeners.Add(value);
        ReplaceHealthDamageListener(listeners);
    }

    public void RemoveHealthDamageListener(IHealthDamageListener value, bool removeComponentWhenEmpty = true) {
        var listeners = healthDamageListener.value;
        listeners.Remove(value);
        if (removeComponentWhenEmpty && listeners.Count == 0) {
            RemoveHealthDamageListener();
        } else {
            ReplaceHealthDamageListener(listeners);
        }
    }
}
