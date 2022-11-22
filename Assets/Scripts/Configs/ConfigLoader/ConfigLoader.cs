using Configs.CircleConfig;
using Configs.GoldByTapConfig;
using Configs.UpgradeConfig;
using UnityEngine;
using Zenject;
using Zenject.Signals;

namespace Configs.ConfigLoader
{
    public abstract class ConfigLoader : MonoBehaviour
    {
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Start()
        {
            LoadAll();
        }

        public abstract IUpgradeConfig GetUpgradeConfig();
        public abstract ICircleConfig GetCircleConfig();
        public abstract IGoldByTapConfig GetGoldByTapConfig();
        protected abstract void LoadAll();

        protected void LoadFinished()
        {
            _signalBus.Fire<LoadFinishedSignal>();
        }
    }
}