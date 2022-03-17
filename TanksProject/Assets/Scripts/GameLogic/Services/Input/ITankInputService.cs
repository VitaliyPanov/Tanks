using Entitas.CodeGeneration.Attributes;
using Tanks.General.Services.Input;

namespace Tanks.GameLogic.Services.Input
{
    [Input, Unique, ComponentName("InputService")]
    public interface ITankInputService : InputControls.ITankActions {}
}