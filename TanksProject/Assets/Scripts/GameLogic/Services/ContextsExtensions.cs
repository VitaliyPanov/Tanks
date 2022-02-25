namespace TanksGB.GameLogic.Services
{
    internal static class ContextsExtensions
    {
        public static void SetTimer(this GameContext context, float lifeTime, int componentIndex,
            GameEntity targetEntity)
        {
            var timerEntity = context.CreateEntity();
            timerEntity.AddTimer(lifeTime);
            timerEntity.AddComponentIndex(componentIndex);
            timerEntity.AddTarget(targetEntity);
        }
    }
}