//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class AIContext {

    public AIEntity maxBallisticDistanceEntity { get { return GetGroup(AIMatcher.MaxBallisticDistance).GetSingleEntity(); } }
    public Tanks.GameLogic.Components.AI.MaxBallisticDistanceComponent maxBallisticDistance { get { return maxBallisticDistanceEntity.maxBallisticDistance; } }
    public bool hasMaxBallisticDistance { get { return maxBallisticDistanceEntity != null; } }

    public AIEntity SetMaxBallisticDistance(float newValue) {
        if (hasMaxBallisticDistance) {
            throw new Entitas.EntitasException("Could not set MaxBallisticDistance!\n" + this + " already has an entity with Tanks.GameLogic.Components.AI.MaxBallisticDistanceComponent!",
                "You should check if the context already has a maxBallisticDistanceEntity before setting it or use context.ReplaceMaxBallisticDistance().");
        }
        var entity = CreateEntity();
        entity.AddMaxBallisticDistance(newValue);
        return entity;
    }

    public void ReplaceMaxBallisticDistance(float newValue) {
        var entity = maxBallisticDistanceEntity;
        if (entity == null) {
            entity = SetMaxBallisticDistance(newValue);
        } else {
            entity.ReplaceMaxBallisticDistance(newValue);
        }
    }

    public void RemoveMaxBallisticDistance() {
        maxBallisticDistanceEntity.Destroy();
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
public partial class AIEntity {

    public Tanks.GameLogic.Components.AI.MaxBallisticDistanceComponent maxBallisticDistance { get { return (Tanks.GameLogic.Components.AI.MaxBallisticDistanceComponent)GetComponent(AIComponentsLookup.MaxBallisticDistance); } }
    public bool hasMaxBallisticDistance { get { return HasComponent(AIComponentsLookup.MaxBallisticDistance); } }

    public void AddMaxBallisticDistance(float newValue) {
        var index = AIComponentsLookup.MaxBallisticDistance;
        var component = (Tanks.GameLogic.Components.AI.MaxBallisticDistanceComponent)CreateComponent(index, typeof(Tanks.GameLogic.Components.AI.MaxBallisticDistanceComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceMaxBallisticDistance(float newValue) {
        var index = AIComponentsLookup.MaxBallisticDistance;
        var component = (Tanks.GameLogic.Components.AI.MaxBallisticDistanceComponent)CreateComponent(index, typeof(Tanks.GameLogic.Components.AI.MaxBallisticDistanceComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveMaxBallisticDistance() {
        RemoveComponent(AIComponentsLookup.MaxBallisticDistance);
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
public sealed partial class AIMatcher {

    static Entitas.IMatcher<AIEntity> _matcherMaxBallisticDistance;

    public static Entitas.IMatcher<AIEntity> MaxBallisticDistance {
        get {
            if (_matcherMaxBallisticDistance == null) {
                var matcher = (Entitas.Matcher<AIEntity>)Entitas.Matcher<AIEntity>.AllOf(AIComponentsLookup.MaxBallisticDistance);
                matcher.componentNames = AIComponentsLookup.componentNames;
                _matcherMaxBallisticDistance = matcher;
            }

            return _matcherMaxBallisticDistance;
        }
    }
}
