using System;
using Configs;
using UnityEngine;
using Zenject;
using Zenject.Signals;

public abstract class ConfigLoader : MonoBehaviour
{
    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus)
    {
        _signalBus = signalBus;
    }

    private void Start()
    {
        LoadAll();
    }

    public abstract IUpgradeConfig GetUpgradeConfig();
    public abstract ICircleConfig GetCircleConfig();
    protected abstract void LoadAll();

    protected void LoadFinished()
    {
        _signalBus.Fire<LoadFinishedSignal>();
    }
}
