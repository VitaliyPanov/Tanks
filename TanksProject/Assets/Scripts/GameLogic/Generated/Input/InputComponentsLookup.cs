//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by Entitas.CodeGeneration.Plugins.ComponentLookupGenerator.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
public static class InputComponentsLookup {

    public const int InputService = 0;
    public const int Attack = 1;
    public const int Direction = 2;
    public const int Pause = 3;
    public const int DeltaTime = 4;
    public const int FixedDeltaTime = 5;
    public const int RealtimeSinceStartup = 6;
    public const int ToggleNext = 7;
    public const int TogglePrevious = 8;

    public const int TotalComponents = 9;

    public static readonly string[] componentNames = {
        "InputService",
        "Attack",
        "Direction",
        "Pause",
        "DeltaTime",
        "FixedDeltaTime",
        "RealtimeSinceStartup",
        "ToggleNext",
        "TogglePrevious"
    };

    public static readonly System.Type[] componentTypes = {
        typeof(InputServiceComponent),
        typeof(Tanks.GameLogic.Components.Input.AttackComponent),
        typeof(Tanks.GameLogic.Components.Input.DirectionComponent),
        typeof(Tanks.GameLogic.Components.Input.PauseComponent),
        typeof(Tanks.GameLogic.Components.Input.Time.DeltaTimeComponent),
        typeof(Tanks.GameLogic.Components.Input.Time.FixedDeltaTimeComponent),
        typeof(Tanks.GameLogic.Components.Input.Time.RealtimeSinceStartupComponent),
        typeof(Tanks.GameLogic.Components.Input.ToggleNextComponent),
        typeof(Tanks.GameLogic.Components.Input.TogglePreviousComponent)
    };
}
