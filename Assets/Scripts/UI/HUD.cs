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

        private SignalBus _signalBus;
        private DiggerManager _diggerManager;
        private DiContainer _container;
    
        [Inject]
        private void Construct(SignalBus signalBus, DiggerManager diggerManager)
        {
            _signalBus = signalBus;
            _diggerManager = diggerManager;
        
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
                _upgradePlayerButton.SetPrice(_diggerManager.GetUpgradePrice(DiggerManager.PlayerDiggerId));
            }
        }

    }
}
