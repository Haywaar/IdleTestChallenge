using System;
using UnityEngine;
using Zenject;
using Zenject.Signals;

public class MoneyManager : MonoBehaviour
{
    [SerializeField] private int _startMoney = 0;
    private NumberData _money;

    public NumberData Money => _money;

    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
        
        _signalBus.Subscribe<AddMoneySignal>(OnMoneyAdd);
        _signalBus.Subscribe<SpendMoneySignal>(OnMoneySpent);
    }

    private void OnMoneyAdd(AddMoneySignal signal)
    {
        _money += signal.Value;

        _signalBus.Fire(new MoneyChangedSignal(_money));
    }

    private void Awake()
    {
        _money = NumberData.FromInt(_startMoney);
    }

    private void Start()
    {
        _signalBus.Fire(new MoneyChangedSignal(_money));
    }

    private void OnMoneySpent(SpendMoneySignal signal)
    {
        _money -= signal.Value;
        _signalBus.Fire(new MoneyChangedSignal(_money));
    }
}
