using System.Collections;
using System.Collections.Generic;
using Digger;
using UI;
using UnityEngine;
using Zenject;
using Zenject.Signals;

public class CircleDiggerWindow : MonoBehaviour
{
   [SerializeField] private UpgradeCircleView _upgradeCircleView;
   [SerializeField] private CircleDiggerView _circleDiggerPrefab;
   [SerializeField] private List<Transform> _circleDiggerPlaceholders;

   private DiContainer _container;
   private SignalBus _signalBus;
   private DiggerManager _diggerManager;
   
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
      _upgradeCircleView.gameObject.SetActive(true);
      _upgradeCircleView.Init(signal.Id, signal.Level, price);
      _upgradeCircleView.transform.position = _circleDiggerPlaceholders[signal.Id - 1].position;
   }
}
