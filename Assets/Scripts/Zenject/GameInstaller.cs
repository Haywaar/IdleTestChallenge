using Configs;
using Digger;
using UnityEngine;
using Zenject.Signals;

namespace Zenject
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private ConfigLoader _configLoader;
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
            
            Container.DeclareSignal<LoadFinishedSignal>().OptionalSubscriber();
        }

        private void BindConfigs()
        {
            Container.Bind<IUpgradeConfig>().FromInstance(_configLoader.GetUpgradeConfig()).AsSingle();
            Container.Bind<ICircleConfig>().FromInstance(_configLoader.GetCircleConfig()).AsSingle();
        }
    }
}