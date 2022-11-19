using System;
using System.Collections.Generic;
using System.Linq;
using Configs;
using UnityEngine;
using Zenject;
using Zenject.Signals;

namespace Digger
{
    public class DiggerManager : MonoBehaviour
    {
        [SerializeField] private CircleConfig _circleConfig;
        [SerializeField] private UpgradeConfig _upgradeConfig;
        [SerializeField] private GoldByTapConfig _goldByTapConfig;

        
        private List<Digger> _diggers = new List<Digger>();

        private SignalBus _signalBus;
        private MoneyManager _moneyManager;

        private PlayerDigger _playerDigger;
        private DiContainer _container;
        
        [Inject]
        private void Construct(SignalBus signalBus, MoneyManager moneyManager, DiContainer container)
        {
            _signalBus = signalBus;
            _moneyManager = moneyManager;
            
            _signalBus.Subscribe<AttackSignal>(OnAttack);
            _container = container;
        }

        private void Awake()
        {
            _playerDigger = new PlayerDigger(0, 1);
            _container.Inject(_playerDigger);
        }

        public NumberData GetUpgradePrice(int diggerId)
        {
            var diggerLevel = _diggers.First(x => x.ID == diggerId).ID;
            return _upgradeConfig.GetUpgradePrice(diggerLevel);
        }

        public void Upgrade(int diggerId)
        {
            var price = GetUpgradePrice(diggerId);
            var diggerLevel = _diggers.First(x => x.ID == diggerId).ID;
            if (_moneyManager.Money > price)
            {
                _signalBus.Fire(new SpendMoneySignal(price));
                UpgradeDigger(diggerId, diggerLevel);
            }
            else
            {
                //TODO - send signal not enough money
            }
        }

        public void BuyCircle()
        {
            var price = GetBuyCirclePrice();
            if (CanBuyCircle())
            {
                _signalBus.Fire(new SpendMoneySignal(price));
                int diggerId = _diggers.Count;
                int level = 1;
                var circleDigger = new CircleDigger(diggerId, level);
                _container.Inject(circleDigger);
                circleDigger.StartAttack();
                
                _diggers.Add(circleDigger);
                _signalBus.Fire(new CircleCreatedSignal(diggerId, level));
            }
            else
            {
                //TODO - send signal not enough money
            }
        }

        public NumberData GetBuyCirclePrice()
        {
            int circlesCount = (_diggers.Count);
            return _circleConfig.GetBuyPrice(circlesCount);
        }
        
        public bool CanBuyCircle()
        {
            var price = GetBuyCirclePrice();
            return _moneyManager.Money >= price && HaveCircleSpots();
        }

        public bool MaxCirclesCount()
        {
            return _diggers.Count == _circleConfig.MaxCirclesCount;
        }

        private bool HaveCircleSpots()
        {
            return (_diggers.Count) < _circleConfig.MaxCirclesCount;
        }
        
        private void OnAttack(AttackSignal signal)
        {
            var gold = _goldByTapConfig.GetGoldByTapValue(signal.Level);
            _signalBus.Fire(new AddMoneySignal(gold));
        }

        private void UpgradeDigger(int id, int newLevel)
        {
            //TODO
        }

        public void OnPlayerClicked()
        {
            _playerDigger.Attack();
        }

    }
}
