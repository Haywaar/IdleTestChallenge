using System.Collections;
using System.Collections.Generic;
using Digger;
using UnityEngine;
using Zenject;
using Zenject.Signals;

public class CircleDiggerWindow : MonoBehaviour
{
   [SerializeField] private CircleDiggerView _circleDiggerPrefab;
   [SerializeField] private List<Transform> _circleDiggerPlaceholders;

   private DiContainer _container;
   private SignalBus _signalBus;
   private List<CircleDiggerView> _circleViews = new List<CircleDiggerView>();
   
   [Inject]
   private void Construct(SignalBus signalBus, DiContainer container)
   {
      _container = container;
      _signalBus = signalBus;
      
      _signalBus.Subscribe<CircleCreatedSignal>(OnCircleCreated);
   }
   
   private void OnCircleCreated(CircleCreatedSignal signal)
   {
      var circleView = Instantiate(_circleDiggerPrefab, _circleDiggerPlaceholders[_circleViews.Count], true);
      _container.Inject(circleView);
      _circleViews.Add(circleView);
      circleView.transform.localPosition = Vector3.zero;

      circleView.Initialize(signal.DiggerId, signal.Level);
   }
}
