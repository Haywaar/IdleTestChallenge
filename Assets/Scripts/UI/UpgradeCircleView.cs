using Digger;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Zenject.Signals;

namespace UI
{
   public class UpgradeCircleView : MonoBehaviour
   {
      [SerializeField] private Image _icon;
      [SerializeField] private Text _upgradePriceText;
   
      [SerializeField] private Button _upgradeButton;
      [SerializeField] private Image _upgradeButtonImage;

      private SignalBus _signalBus;
      private DiggerManager _diggerManager;
      private int _diggerId;
      private int _level;
   
      [Inject]
      private void Construct(SignalBus signalBus, DiggerManager diggerManager)
      {
         _signalBus = signalBus;
         _diggerManager = diggerManager;
      
         _signalBus.Subscribe<MoneyChangedSignal>(OnMoneyChanged);
      }

      private void OnMoneyChanged(MoneyChangedSignal signal)
      {
         if (gameObject.activeSelf)
         {
            SetUpgradeButtonInteractable(_diggerManager.CanUpgrade(_diggerId));
         }
      }

      public void Init(int diggerId, int level, NumberData.NumberData price)
      {
         _diggerId = diggerId;
         _level = level;

         _upgradePriceText.text = price.ToString();
         SetUpgradeButtonInteractable(_diggerManager.CanUpgrade(_diggerId));
      
         _upgradeButton.onClick.RemoveAllListeners();
         _upgradeButton.onClick.AddListener( (() =>
         {
            _diggerManager.Upgrade(_diggerId);
            gameObject.SetActive(false);
         }));
      }

      private void SetUpgradeButtonInteractable(bool isActive)
      {
         _upgradeButton.interactable = isActive;
         _upgradeButtonImage.color = isActive ? Color.white : Color.gray;
      }
   }
}
