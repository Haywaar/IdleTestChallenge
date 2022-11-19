using Digger;
using UnityEngine;
using Zenject.Signals;

namespace Zenject
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private DiggerManager _diggerManager;
        [SerializeField] private MoneyManager _moneyManager;

        public override void InstallBindings()
        {
            Container.Bind<DiggerManager>().FromInstance(_diggerManager).AsSingle();
            Container.Bind<MoneyManager>().FromInstance(_moneyManager).AsSingle();
            BindSignals();
        }

        private void BindSignals()
        {
            SignalBusInstaller.Install(Container);
            Container.DeclareSignal<AttackSignal>().OptionalSubscriber();
            Container.DeclareSignal<CircleCreatedSignal>().OptionalSubscriber();
            
            Container.DeclareSignal<AddMoneySignal>().OptionalSubscriber();
            Container.DeclareSignal<SpendMoneySignal>().OptionalSubscriber();
            Container.DeclareSignal<MoneyChangedSignal>().OptionalSubscriber();
        }
    }
}