//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputContext {

    public InputEntity realtimeSinceStartupEntity { get { return GetGroup(InputMatcher.RealtimeSinceStartup).GetSingleEntity(); } }
    public Tanks.GameLogic.Components.Input.Time.RealtimeSinceStartupComponent realtimeSinceStartup { get { return realtimeSinceStartupEntity.realtimeSinceStartup; } }
    public bool hasRealtimeSinceStartup { get { return realtimeSinceStartupEntity != null; } }

    public InputEntity SetRealtimeSinceStartup(float newValue) {
        if (hasRealtimeSinceStartup) {
            throw new Entitas.EntitasException("Could not set RealtimeSinceStartup!\n" + this + " already has an entity with Tanks.GameLogic.Components.Input.Time.RealtimeSinceStartupComponent!",
                "You should check if the context already has a realtimeSinceStartupEntity before setting it or use context.ReplaceRealtimeSinceStartup().");
        }
        var entity = CreateEntity();
        entity.AddRealtimeSinceStartup(newValue);
        return entity;
    }

    public void ReplaceRealtimeSinceStartup(float newValue) {
        var entity = realtimeSinceStartupEntity;
        if (entity == null) {
            entity = SetRealtimeSinceStartup(newValue);
        } else {
            entity.ReplaceRealtimeSinceStartup(newValue);
        }
    }

    public void RemoveRealtimeSinceStartup() {
        realtimeSinceStartupEntity.Destroy();
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

    public Tanks.GameLogic.Components.Input.Time.RealtimeSinceStartupComponent realtimeSinceStartup { get { return (Tanks.GameLogic.Components.Input.Time.RealtimeSinceStartupComponent)GetComponent(InputComponentsLookup.RealtimeSinceStartup); } }
    public bool hasRealtimeSinceStartup { get { return HasComponent(InputComponentsLookup.RealtimeSinceStartup); } }

    public void AddRealtimeSinceStartup(float newValue) {
        var index = InputComponentsLookup.RealtimeSinceStartup;
        var component = (Tanks.GameLogic.Components.Input.Time.RealtimeSinceStartupComponent)CreateComponent(index, typeof(Tanks.GameLogic.Components.Input.Time.RealtimeSinceStartupComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceRealtimeSinceStartup(float newValue) {
        var index = InputComponentsLookup.RealtimeSinceStartup;
        var component = (Tanks.GameLogic.Components.Input.Time.RealtimeSinceStartupComponent)CreateComponent(index, typeof(Tanks.GameLogic.Components.Input.Time.RealtimeSinceStartupComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveRealtimeSinceStartup() {
        RemoveComponent(InputComponentsLookup.RealtimeSinceStartup);
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

    static Entitas.IMatcher<InputEntity> _matcherRealtimeSinceStartup;

    public static Entitas.IMatcher<InputEntity> RealtimeSinceStartup {
        get {
            if (_matcherRealtimeSinceStartup == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.RealtimeSinceStartup);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherRealtimeSinceStartup = matcher;
            }

            return _matcherRealtimeSinceStartup;
        }
    }
}
