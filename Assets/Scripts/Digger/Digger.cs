using UnityEngine;
using Zenject;
using Zenject.Signals;

namespace Digger
{
    public abstract class Digger
    {
        protected int _id;

        protected int _level;

        protected SignalBus _signalBus;

        public int ID => _id;

        public int Level => _level;

        [Inject]
        protected void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
            
            _signalBus.Subscribe<UpgradeDiggerSignal>(OnUpgrade);
            OnInjected();
        }

        private void OnUpgrade(UpgradeDiggerSignal signal)
        {
            if (signal.Id == _id)
            {
                _level = signal.Level;
            }
        }

        public Digger(int id, int level)
        {
            _id = id;
            _level = level;
        }

        public void Attack()
        {
            _signalBus.Fire(new AttackSignal(_id, _level));
        }

        protected virtual void OnInjected()
        {
        }
    }
}