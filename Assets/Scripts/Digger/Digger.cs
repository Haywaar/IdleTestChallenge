using UnityEngine;
using Zenject;
using Zenject.Signals;

namespace Digger
{
    public abstract class Digger
    {
        protected int _id;

        protected int _level;

        private SignalBus _signalBus;

        public int ID => _id;

        public int Level => _level;

        [Inject]
        protected void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
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

        protected virtual void OnUpgrade(int id, int level)
        {
            _level = level;
        }
    }
}