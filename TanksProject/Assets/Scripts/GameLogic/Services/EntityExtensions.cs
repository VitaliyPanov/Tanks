namespace TanksGB.GameLogic.Services
{
    internal static class EntityExtensions
    {
        public static void SetHealth(this GameEntity entity, float maxValue, float currentValue = 0)
        {
            entity.isHealth = true;
            entity.ReplaceMaxHealth(maxValue);
            entity.ReplaceCurrentHealth(currentValue == 0 ? maxValue : currentValue);
        }
    }
}