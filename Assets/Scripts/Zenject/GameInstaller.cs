using Configs;
using Digger;
using UnityEngine;
using Zenject.Signals;

namespace Zenject
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private UpgradeConfigScriptableObject _upgradeConfigScriptableObject;
        [SerializeField] private UpgradeConfigRemoteFile _upgradeConfigRemoteFile;
        [SerializeField] private bool isRemoteConfigs;
        
        [SerializeField] private DiggerManager _diggerManager;
        [SerializeField] private MoneyManager _moneyManager;

        public override void InstallBindings()
        {
            Container.Bind<DiggerManager>().FromInstance(_diggerManager).AsSingle();
            Container.Bind<MoneyManager>().FromInstance(_moneyManager).AsSingle();
            BindSignals();
            BindConfigs();
        }

        private void BindSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<AttackSignal>().OptionalSubscriber();
            Container.DeclareSignal<CircleCreatedSignal>().OptionalSubscriber();
            Container.DeclareSignal<CircleClickedSignal>().OptionalSubscriber();
            Container.DeclareSignal<PlayerClickedSignal>().OptionalSubscriber();
            Container.DeclareSignal<UpgradeDiggerSignal>().OptionalSubscriber();
            
            Container.DeclareSignal<AddMoneySignal>().OptionalSubscriber();
            Container.DeclareSignal<SpendMoneySignal>().OptionalSubscriber();
            Container.DeclareSignal<MoneyChangedSignal>().OptionalSubscriber();
            
            Container.DeclareSignal<GameStartedSignal>().OptionalSubscriber();
        }

        private void BindConfigs()
        {
            if (isRemoteConfigs)
            {
                Debug.LogWarning("Bind remote!");
                Container.Bind<IUpgradeConfig>().FromInstance(_upgradeConfigRemoteFile).AsSingle();
            }
            else
            {
                Container.Bind<IUpgradeConfig>().FromInstance(_upgradeConfigScriptableObject).AsSingle();
            }
        }
    }
}