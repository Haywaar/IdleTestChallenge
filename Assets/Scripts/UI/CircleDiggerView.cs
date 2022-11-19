using UnityEngine;
using Zenject;
using Zenject.Signals;

namespace Digger
{
    public class CircleDiggerView : MonoBehaviour
    {
        private SignalBus _signalBus;

        private int _diggerId;
        private int _level;
        
        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Initialize(int diggerId, int level)
        {
            _signalBus.Subscribe<AttackSignal>(VisualizeAttack);
            _diggerId = diggerId;
            _level = level;
        }

        private void VisualizeAttack(AttackSignal signal)
        {
            if (signal.DiggerId == _diggerId)
            {
                //TODO - anim
            }
        }
    }
}