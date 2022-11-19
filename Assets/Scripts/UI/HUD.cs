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
        [SerializeField] private BuyCircleButton _buyCircleButton;

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
        }

        private void OnCircleCreated()
        {
            _buyCircleButton.SetPrice(_diggerManager.GetBuyCirclePrice());
        }

        private void Awake()
        {
            _buyCircleButton.Init(BuyCircleButtonClicked, _diggerManager.GetBuyCirclePrice());
        }

        private void BuyCircleButtonClicked()
        {
          _diggerManager.BuyCircle();
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

        }
    }
}
