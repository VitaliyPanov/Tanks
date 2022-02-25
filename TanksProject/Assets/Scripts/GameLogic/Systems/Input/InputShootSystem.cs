using Entitas;

namespace TanksGB.GameLogic.Systems.Input
{
    internal sealed class InputShootSystem : IExecuteSystem
    {
        private readonly InputContext _inputContext;
        private readonly GameContext _gameContext;

        public InputShootSystem(Contexts contexts)
        {
            _gameContext = contexts.game;
            _inputContext = contexts.input;
        }
        public void Execute()
        {
            GameEntity controllableEntity = _gameContext.controllable.Entity;
            if (!controllableEntity.isWeaponFired)
                controllableEntity.isWeaponActivate = _inputContext.isAttack;
        }
    }
}