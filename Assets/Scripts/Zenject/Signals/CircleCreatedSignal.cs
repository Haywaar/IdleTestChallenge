namespace Zenject.Signals
{
    public class CircleCreatedSignal
    {
        public readonly int DiggerId;
        public readonly int Level;
        
        public CircleCreatedSignal(int diggerId, int level)
        {
            DiggerId = diggerId;
            Level = level;
        }
    }
}