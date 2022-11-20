using Zenject.Signals;

namespace Digger
{
    public class PlayerDigger : Digger
    {
        public PlayerDigger(int id, int level) : base(id, level)
        {
        }

        protected override void OnInjected()
        {
            _signalBus.Subscribe<PlayerClickedSignal>(_ => Attack());
        }
    }
}