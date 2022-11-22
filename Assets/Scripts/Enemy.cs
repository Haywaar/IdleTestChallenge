using System;
using UnityEngine;
using Zenject;
using Zenject.Signals;

public class Enemy : MonoBehaviour
{
   private SignalBus _signalBus;
   [Inject]
   private void Construct(SignalBus signalBus)
   {
      _signalBus = signalBus;
   }

   private void Awake()
   {
      _signalBus.Subscribe<AttackSignal>(VisualizeAttack);
   }

   private void VisualizeAttack(AttackSignal signal)
   {
      //
   }
}
