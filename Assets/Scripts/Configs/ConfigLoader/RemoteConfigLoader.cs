using Configs.CircleConfig;
using Configs.GoldByTapConfig;
using Configs.UpgradeConfig;
using UnityEngine;

namespace Configs.ConfigLoader
{
    public class RemoteConfigLoader : ConfigLoader
    {
        private UpgradeConfigRemote _upgradeConfig = new();
        private CircleConfigRemote _circleConfig = new();
        private GoldByTapConfigRemote _goldByTapConfig = new();

        [SerializeField] private string _upgradeConfigName = "UpgradeConfig.json";
        [SerializeField] private string _circleConfigName = "CircleConfig.json";
        [SerializeField] private string _goldByTapConfigName = "GoldByTapConfig.json";
        
        public override IUpgradeConfig GetUpgradeConfig()
        {
            return _upgradeConfig;
        }

        public override ICircleConfig GetCircleConfig()
        {
            return _circleConfig;
        }

        public override IGoldByTapConfig GetGoldByTapConfig()
        {
            return _goldByTapConfig;
        }

        protected override async void LoadAll()
        {
            await _upgradeConfig.LoadData(_upgradeConfigName);
            await _circleConfig.LoadData(_circleConfigName);
            await _goldByTapConfig.LoadData(_goldByTapConfigName);
            LoadFinished();
        }
    }
}