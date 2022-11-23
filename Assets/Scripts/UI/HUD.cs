using System.Collections.Generic;
using Configs;
using Digger;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Zenject.Signals;

namespace UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private Text _goldText;
        [SerializeField] private ButtonWithPrice _buyCircleButton;
        [SerializeField] private ButtonWithPrice _upgradePlayerButton;

        [SerializeField] private Text _playerLevelText;
        [SerializeField] private Image _playerBladeImage;

        private SignalBus _signalBus;
        private DiggerManager _diggerManager;
        private LevelColorConfig _colorConfig;
    
        [Inject]
        private void Construct(SignalBus signalBus, DiggerManager diggerManager, LevelColorConfig colorConfig)
        {
            _signalBus = signalBus;
            _diggerManager = diggerManager;
            _colorConfig = colorConfig;
        
            _signalBus.Subscribe<MoneyChangedSignal>(OnMoneyChanged);
            _signalBus.Subscribe<CircleCreatedSignal>(OnCircleCreated);
            _signalBus.Subscribe<UpgradeDiggerSignal>(OnDiggerUpdated);
            _signalBus.Subscribe<LoadFinishedSignal>(OnGameStarted);
        }

       
        private void OnCircleCreated()
        {
            _buyCircleButton.SetPrice(_diggerManager.GetBuyCirclePrice());
        }

        private void OnGameStarted()
        {
            _buyCircleButton.Init(BuyCircleButtonClicked, _diggerManager.GetBuyCirclePrice());
            _upgradePlayerButton.Init(UpgradePlayerButtonClicked, _diggerManager.GetUpgradePrice(DiggerManager.PlayerDiggerId));
            
            _buyCircleButton.SetInteractable(_diggerManager.CanBuyCircle());
            _upgradePlayerButton.SetInteractable(_diggerManager.CanUpgrade(DiggerManager.PlayerDiggerId));
            
            _playerLevelText.text = "Level: " + _diggerManager.PlayerDigger.Level;
            PaintBladeColor(_diggerManager.PlayerDigger.Level);
        }

        private void BuyCircleButtonClicked()
        {
          _diggerManager.BuyCircle();
        }
        
        private void UpgradePlayerButtonClicked()
        {
            _diggerManager.Upgrade(DiggerManager.PlayerDiggerId);
        }

        private void OnMoneyChanged(MoneyChangedSignal signal)
        {
            _goldText.text = "Gold: " + signal.Value;

            if (_diggerManager.MaxCirclesCount())
            {
                _buyCircleButton.SetMax();
            }
            else
            {
                _buyCircleButton.SetInteractable(_diggerManager.CanBuyCircle());
            }

            _upgradePlayerButton.SetInteractable(_diggerManager.CanUpgrade(DiggerManager.PlayerDiggerId));

        }
        
        private void OnDiggerUpdated(UpgradeDiggerSignal signal)
        {
            if (DiggerManager.PlayerDiggerId == signal.Id)
            {
                _upgradePlayerButton.SetPrice(_diggerManager.GetUpgradePriceByLevel(signal.Level));
                _playerLevelText.text = "Level: " + signal.Level;
                PaintBladeColor(signal.Level);
            }
        }

        private void PaintBladeColor(int level)
        {
            _playerBladeImage.color = _colorConfig.GetColorForLevel(level);
        }
    }
}
