namespace Tanks.GameLogic.Services
{
    public static class ContextsExtensions
    {
        public static void SetTimer(this GameContext context, GameEntity targetEntity, int componentIndex,
            float lifeTime)
        {
            var timerEntity = context.CreateEntity();
            timerEntity.AddTimer(lifeTime);
            timerEntity.AddComponentIndex(componentIndex);
            timerEntity.AddTarget(targetEntity);
        }
    }
}