using UnityEngine;

namespace Zenject.Signals
{
    public class AddMoneySignal
    {
        public readonly NumberData Value;
        public readonly int Level;

        public AddMoneySignal(NumberData value, int level)
        {
            Value = value;
            Level = level;
        }
    }
}
