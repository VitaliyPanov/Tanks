﻿using Entitas;
using Entitas.CodeGeneration.Attributes;

namespace Tanks.GameLogic.Components.Game
{
    [Game, FlagPrefix("try")]
    public sealed class ControlComponent : IComponent {}
}