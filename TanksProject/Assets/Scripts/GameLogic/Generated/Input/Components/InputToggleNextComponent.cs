//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class InputContext {

    public InputEntity toggleNextEntity { get { return GetGroup(InputMatcher.ToggleNext).GetSingleEntity(); } }

    public bool isToggleNext {
        get { return toggleNextEntity != null; }
        set {
            var entity = toggleNextEntity;
            if (value != (entity != null)) {
                if (value) {
                    CreateEntity().isToggleNext = true;
                } else {
                    entity.Destroy();
                }
            }
        }
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

    static readonly Tanks.GameLogic.Components.Input.ToggleNextComponent toggleNextComponent = new Tanks.GameLogic.Components.Input.ToggleNextComponent();

    public bool isToggleNext {
        get { return HasComponent(InputComponentsLookup.ToggleNext); }
        set {
            if (value != isToggleNext) {
                var index = InputComponentsLookup.ToggleNext;
                if (value) {
                    var componentPool = GetComponentPool(index);
                    var component = componentPool.Count > 0
                            ? componentPool.Pop()
                            : toggleNextComponent;

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
public sealed partial class InputMatcher {

    static Entitas.IMatcher<InputEntity> _matcherToggleNext;

    public static Entitas.IMatcher<InputEntity> ToggleNext {
        get {
            if (_matcherToggleNext == null) {
                var matcher = (Entitas.Matcher<InputEntity>)Entitas.Matcher<InputEntity>.AllOf(InputComponentsLookup.ToggleNext);
                matcher.componentNames = InputComponentsLookup.componentNames;
                _matcherToggleNext = matcher;
            }

            return _matcherToggleNext;
        }
    }
}
