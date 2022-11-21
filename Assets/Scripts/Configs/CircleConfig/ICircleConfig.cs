namespace Configs
{
    public interface ICircleConfig
    {
        public NumberData.NumberData GetBuyPrice(int circlesCount);
        
        public int GetMaxCirclesCount();

        public float GetCircleAttackCooldown();
    }
}