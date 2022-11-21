using System.Collections;
using System.Collections.Generic;
using Configs;
using UnityEngine;

public class LocalConfigLoader : ConfigLoader
{
    [SerializeField] private UpgradeConfigScriptableObject _upgradeConfigScriptableObject;
    [SerializeField] private CircleConfigScriptableObject _circleConfigScriptableObject;
    public override IUpgradeConfig GetUpgradeConfig()
    {
        return _upgradeConfigScriptableObject;
    }

    public override ICircleConfig GetCircleConfig()
    {
        return _circleConfigScriptableObject;
    }

    protected override void LoadAll()
    {
        LoadFinished();
    }
}
