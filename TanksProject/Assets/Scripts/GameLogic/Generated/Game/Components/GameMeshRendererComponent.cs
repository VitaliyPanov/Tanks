//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentEntityApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameEntity {

    public Tanks.GameLogic.Components.Game.MeshRendererComponent meshRenderer { get { return (Tanks.GameLogic.Components.Game.MeshRendererComponent)GetComponent(GameComponentsLookup.MeshRenderer); } }
    public bool hasMeshRenderer { get { return HasComponent(GameComponentsLookup.MeshRenderer); } }

    public void AddMeshRenderer(UnityEngine.MeshRenderer[] newArray) {
        var index = GameComponentsLookup.MeshRenderer;
        var component = (Tanks.GameLogic.Components.Game.MeshRendererComponent)CreateComponent(index, typeof(Tanks.GameLogic.Components.Game.MeshRendererComponent));
        component.Array = newArray;
        AddComponent(index, component);
    }

    public void ReplaceMeshRenderer(UnityEngine.MeshRenderer[] newArray) {
        var index = GameComponentsLookup.MeshRenderer;
        var component = (Tanks.GameLogic.Components.Game.MeshRendererComponent)CreateComponent(index, typeof(Tanks.GameLogic.Components.Game.MeshRendererComponent));
        component.Array = newArray;
        ReplaceComponent(index, component);
    }

    public void RemoveMeshRenderer() {
        RemoveComponent(GameComponentsLookup.MeshRenderer);
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

    static Entitas.IMatcher<GameEntity> _matcherMeshRenderer;

    public static Entitas.IMatcher<GameEntity> MeshRenderer {
        get {
            if (_matcherMeshRenderer == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.MeshRenderer);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherMeshRenderer = matcher;
            }

            return _matcherMeshRenderer;
        }
    }
}
