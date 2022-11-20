using UnityEngine;

namespace Zenject.Signals
{
    public class CircleClickedSignal
    {
        public readonly int Id;
        public readonly int Level;

        public CircleClickedSignal(int id, int level)
        {
            Id = id;
            Level = level;
        }
    }
}