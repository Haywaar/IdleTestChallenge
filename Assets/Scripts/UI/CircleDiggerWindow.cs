using System.Collections.Generic;
using Digger;
using UnityEngine;
using Zenject;
using Zenject.Signals;

namespace UI
{
   public class CircleDiggerWindow : MonoBehaviour
   {
      [SerializeField] private UpgradeCircleView _upgradeCircleViewPrefab;
      [SerializeField] private CircleDiggerView _circleDiggerPrefab;
      [SerializeField] private List<Transform> _circleDiggerPlaceholders;

      private DiContainer _container;
      private SignalBus _signalBus;
      private DiggerManager _diggerManager;

      private UpgradeCircleView _upgradeCircleView;
   
      private List<CircleDiggerView> _circleViews = new List<CircleDiggerView>();
   
      [Inject]
      private void Construct(SignalBus signalBus, DiContainer container, DiggerManager diggerManager)
      {
         _container = container;
         _signalBus = signalBus;
         _diggerManager = diggerManager;
      
         _signalBus.Subscribe<CircleCreatedSignal>(OnCircleCreated);
         _signalBus.Subscribe<CircleClickedSignal>(OnCircleClicked);
      }

      private void OnCircleCreated(CircleCreatedSignal signal)
      {
         var circleView = Instantiate(_circleDiggerPrefab, _circleDiggerPlaceholders[_circleViews.Count], true);
         _container.Inject(circleView);
         _circleViews.Add(circleView);
         circleView.transform.localPosition = Vector3.zero;
         circleView.transform.localScale = Vector3.one;

         circleView.Initialize(signal.DiggerId, signal.Level);
      }
   
      private void OnCircleClicked(CircleClickedSignal signal)
      {
         var price = _diggerManager.GetUpgradePrice(signal.Id);

         if (_upgradeCircleView == null)
         {
            _upgradeCircleView = Instantiate(_upgradeCircleViewPrefab, transform);
            _container.Inject(_upgradeCircleView);
         }
      
         _upgradeCircleView.gameObject.SetActive(true);
         _upgradeCircleView.Init(signal.Id, signal.Level, price);
         _upgradeCircleView.transform.position = _circleDiggerPlaceholders[signal.Id - 1].position;
      }
   }
}
