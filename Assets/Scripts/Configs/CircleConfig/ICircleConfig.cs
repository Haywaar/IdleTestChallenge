namespace Configs.CircleConfig
{
    public interface ICircleConfig
    {
        public NumberData GetBuyPrice(int circlesCount);
        
        public int GetMaxCirclesCount();

        public float GetCircleAttackCooldown();
    }
}