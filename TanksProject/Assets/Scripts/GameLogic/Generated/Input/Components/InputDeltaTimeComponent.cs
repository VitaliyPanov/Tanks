//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputContext {

    public InputEntity deltaTimeEntity { get { return GetGroup(InputMatcher.DeltaTime).GetSingleEntity(); } }
    public TanksGB.GameLogic.Components.Input.Time.DeltaTimeComponent deltaTime { get { return deltaTimeEntity.deltaTime; } }
    public bool hasDeltaTime { get { return deltaTimeEntity != null; } }

    public InputEntity SetDeltaTime(float newValue) {
        if (hasDeltaTime) {
            throw new Entitas.EntitasException("Could not set DeltaTime!\n" + this + " already has an entity with TanksGB.GameLogic.Components.Input.Time.DeltaTimeComponent!",
                "You should check if the context already has a deltaTimeEntity before setting it or use context.ReplaceDeltaTime().");
        }
        var entity = CreateEntity();
        entity.AddDeltaTime(newValue);
        return entity;
    }

    public void ReplaceDeltaTime(float newValue) {
        var entity = deltaTimeEntity;
        if (entity == null) {
            entity = SetDeltaTime(newValue);
        } else {
            entity.ReplaceDeltaTime(newValue);
        }
    }

    public void RemoveDeltaTime() {
        deltaTimeEntity.Destroy();
    }
}

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputEntity {

    public TanksGB.GameLogic.Components.Input.Time.DeltaTimeComponent deltaTime { get { return (TanksGB.GameLogic.Components.Input.Time.DeltaTimeComponent)GetComponent(InputComponentsLookup.DeltaTime); } }
    public bool hasDeltaTime { get { return HasComponent(InputComponentsLookup.DeltaTime); } }

    public void AddDeltaTime(float newValue) {
        var index = InputComponentsLookup.DeltaTime;
        var component = (TanksGB.GameLogic.Components.Input.Time.DeltaTimeComponent)CreateComponent(index, typeof(TanksGB.GameLogic.Components.Input.Time.DeltaTimeComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceDeltaTime(float newValue) {
        var index = InputComponentsLookup.DeltaTime;
        var component = (TanksGB.GameLogic.Components.Input.Time.DeltaTimeComponent)CreateComponent(index, typeof(TanksGB.GameLogic.Components.Input.Time.DeltaTimeComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveDeltaTime() {
        RemoveComponent(InputComponentsLookup.DeltaTime);
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
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherDeltaTime;

    public static Entitas.IMatcher<InputEntity> DeltaTime {
        get {
            if (_matcherDeltaTime == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.DeltaTime);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherDeltaTime = matcher;
            }

            return _matcherDeltaTime;
        }
    }
}
