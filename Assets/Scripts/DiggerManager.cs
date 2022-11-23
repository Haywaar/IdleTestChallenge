using System;
using System.Collections.Generic;
using System.Linq;
using Configs;
using Configs.CircleConfig;
using Configs.GoldByTapConfig;
using Configs.UpgradeConfig;
using UnityEngine;
using Zenject;
using Zenject.Signals;

namespace Digger
{
    public class DiggerManager : MonoBehaviour
    {
        private IGoldByTapConfig _goldByTapConfig;
        private IUpgradeConfig _upgradeConfig;
        private ICircleConfig _circleConfig;

        public const int PlayerDiggerId = 0;
        private List<Digger> _diggers = new List<Digger>();

        private SignalBus _signalBus;
        private MoneyManager _moneyManager;

        private PlayerDigger _playerDigger;

        public PlayerDigger PlayerDigger => _playerDigger;

        private DiContainer _container;

        [Inject]
        private void Construct(SignalBus signalBus, MoneyManager moneyManager, DiContainer container,
            IUpgradeConfig upgradeConfig, ICircleConfig circleConfig, IGoldByTapConfig goldByTapConfig)
        {
            _signalBus = signalBus;
            _moneyManager = moneyManager;
            
            _upgradeConfig = upgradeConfig;
            _circleConfig = circleConfig;
            _goldByTapConfig = goldByTapConfig;

            _signalBus.Subscribe<AttackSignal>(OnAttack);
            _container = container;
        }

        private void Awake()
        {
            _playerDigger = new PlayerDigger(0, 1);
            _container.Inject(_playerDigger);

            _diggers.Add(_playerDigger);
        }

        public void BuyCircle()
        {
            var price = GetBuyCirclePrice();
            if (CanBuyCircle())
            {
                int diggerId = _diggers.Count;
                int level = 1;

                var circleDigger = new CircleDigger(diggerId, level, _circleConfig.GetCircleAttackCooldown());
                _container.Inject(circleDigger);
                circleDigger.StartAttack();

                _diggers.Add(circleDigger);
                _signalBus.Fire(new CircleCreatedSignal(diggerId, level));
                
                _signalBus.Fire(new SpendMoneySignal(price));
            }
            else
            {
                //TODO - send signal not enough money
            }
        }

        public NumberData GetBuyCirclePrice()
        {
            int circlesCount = (_diggers.Count - 1);
            return _circleConfig.GetBuyPrice(circlesCount);
        }

        public bool CanBuyCircle()
        {
            var price = GetBuyCirclePrice();
            return _moneyManager.Money >= price && HaveCircleSpots();
        }

        public bool MaxCirclesCount()
        {
            return _diggers.Count == _circleConfig.GetMaxCirclesCount() + 1;
        }

        private bool HaveCircleSpots()
        {
            return (_diggers.Count) < _circleConfig.GetMaxCirclesCount() + 1;
        }

        private void OnAttack(AttackSignal signal)
        {
            var gold = _goldByTapConfig.GetGoldByTapValue(signal.Level);
            _signalBus.Fire(new AddMoneySignal(gold, signal.Level));
        }

        public NumberData GetUpgradePrice(int diggerId)
        {
            var diggerLevel = _diggers.First(x => x.ID == diggerId).Level;
            return _upgradeConfig.GetUpgradePrice(diggerLevel);
        }
        
        public NumberData GetUpgradePriceByLevel(int diggerLevel)
        {
            return _upgradeConfig.GetUpgradePrice(diggerLevel);
        }

        public void Upgrade(int diggerId)
        {
            var price = GetUpgradePrice(diggerId);
            if (_moneyManager.Money >= price)
            {
                var newLevel = _diggers.First(x => x.ID == diggerId).Level + 1;
                _signalBus.Fire(new UpgradeDiggerSignal(diggerId, newLevel));
                _signalBus.Fire(new SpendMoneySignal(price));
            }
            else
            {
                //TODO - send signal not enough money
            }
        }

        public bool CanUpgrade(int diggerId)
        {
            var price = GetUpgradePrice(diggerId);
            return _moneyManager.Money >= price;
        }
    }
}