namespace Zenject.Signals
{
    public class AttackSignal
    {
        public readonly int DiggerId;
        public readonly int Level;

        public AttackSignal(int diggerId, int level)
        {
            DiggerId = diggerId;
            Level = level;
        }

    }
}
