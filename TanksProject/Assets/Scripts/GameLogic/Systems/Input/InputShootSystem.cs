using Entitas;

namespace Tanks.GameLogic.Systems.Input
{
    internal sealed class InputShootSystem : IExecuteSystem
    {
        private readonly InputContext _inputContext;
        private readonly GameContext _gameContext;

        public InputShootSystem(GameContext gameContext, InputContext inputContext)
        {
            _gameContext = gameContext;
            _inputContext = inputContext;
        }
        public void Execute()
        {
            GameEntity controllableEntity = _gameContext.controllable.Entity;
            if (!controllableEntity.isWeaponFired)
                controllableEntity.isWeaponActivate = _inputContext.isAttack;
        }
    }
}