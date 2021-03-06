//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentContextApiGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public partial class GameContext {

    public GameEntity winnersTeamEntity { get { return GetGroup(GameMatcher.WinnersTeam).GetSingleEntity(); } }
    public Tanks.GameLogic.Components.Game.WinnersTeamComponent winnersTeam { get { return winnersTeamEntity.winnersTeam; } }
    public bool hasWinnersTeam { get { return winnersTeamEntity != null; } }

    public GameEntity SetWinnersTeam(Tanks.Data.TeamType newValue) {
        if (hasWinnersTeam) {
            throw new Entitas.EntitasException("Could not set WinnersTeam!\n" + this + " already has an entity with Tanks.GameLogic.Components.Game.WinnersTeamComponent!",
                "You should check if the context already has a winnersTeamEntity before setting it or use context.ReplaceWinnersTeam().");
        }
        var entity = CreateEntity();
        entity.AddWinnersTeam(newValue);
        return entity;
    }

    public void ReplaceWinnersTeam(Tanks.Data.TeamType newValue) {
        var entity = winnersTeamEntity;
        if (entity == null) {
            entity = SetWinnersTeam(newValue);
        } else {
            entity.ReplaceWinnersTeam(newValue);
        }
    }

    public void RemoveWinnersTeam() {
        winnersTeamEntity.Destroy();
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
public partial class GameEntity {

    public Tanks.GameLogic.Components.Game.WinnersTeamComponent winnersTeam { get { return (Tanks.GameLogic.Components.Game.WinnersTeamComponent)GetComponent(GameComponentsLookup.WinnersTeam); } }
    public bool hasWinnersTeam { get { return HasComponent(GameComponentsLookup.WinnersTeam); } }

    public void AddWinnersTeam(Tanks.Data.TeamType newValue) {
        var index = GameComponentsLookup.WinnersTeam;
        var component = (Tanks.GameLogic.Components.Game.WinnersTeamComponent)CreateComponent(index, typeof(Tanks.GameLogic.Components.Game.WinnersTeamComponent));
        component.Value = newValue;
        AddComponent(index, component);
    }

    public void ReplaceWinnersTeam(Tanks.Data.TeamType newValue) {
        var index = GameComponentsLookup.WinnersTeam;
        var component = (Tanks.GameLogic.Components.Game.WinnersTeamComponent)CreateComponent(index, typeof(Tanks.GameLogic.Components.Game.WinnersTeamComponent));
        component.Value = newValue;
        ReplaceComponent(index, component);
    }

    public void RemoveWinnersTeam() {
        RemoveComponent(GameComponentsLookup.WinnersTeam);
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

    static Entitas.IMatcher<GameEntity> _matcherWinnersTeam;

    public static Entitas.IMatcher<GameEntity> WinnersTeam {
        get {
            if (_matcherWinnersTeam == null) {
                var matcher = (Entitas.Matcher<GameEntity>)Entitas.Matcher<GameEntity>.AllOf(GameComponentsLookup.WinnersTeam);
                matcher.componentNames = GameComponentsLookup.componentNames;
                _matcherWinnersTeam = matcher;
            }

            return _matcherWinnersTeam;
        }
    }
}
