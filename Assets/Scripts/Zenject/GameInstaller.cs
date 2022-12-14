using Configs;
using Configs.CircleConfig;
using Configs.ConfigLoader;
using Configs.GoldByTapConfig;
using Configs.UpgradeConfig;
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
        [SerializeField] private LevelColorConfig _colorConfig;
        [SerializeField] private EnemyView _enemyView;
        
        public override void InstallBindings()
        {
            Container.Bind<DiggerManager>().FromInstance(_diggerManager).AsSingle();
            Container.Bind<MoneyManager>().FromInstance(_moneyManager).AsSingle();
            
            BindSignals();
            BindConfigs();
            BindUI();
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
            Container.Bind<IGoldByTapConfig>().FromInstance(_configLoader.GetGoldByTapConfig()).AsSingle();
            
            Container.Bind<LevelColorConfig>().FromInstance(_colorConfig).AsSingle();
        }

        private void BindUI()
        {
            Container.Bind<EnemyView>().FromInstance(_enemyView).AsSingle();
        }
    }
}