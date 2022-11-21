using System.Collections;
using System.Collections.Generic;
using Configs;
using UnityEngine;

public class RemoteConfigLoader : ConfigLoader
{
    private UpgradeConfigRemoteFile _upgradeConfigRemoteFile = new();
    private CircleConfigRemote _circleConfigRemote = new();
    public override IUpgradeConfig GetUpgradeConfig()
    {
        return _upgradeConfigRemoteFile;
    }

    public override ICircleConfig GetCircleConfig()
    {
        return _circleConfigRemote;
    }

    protected override async void LoadAll()
    {
        await _upgradeConfigRemoteFile.LoadData("UpgradeConfig.json");
        await _circleConfigRemote.LoadData("CircleConfig.json");
        LoadFinished();
    }
}
