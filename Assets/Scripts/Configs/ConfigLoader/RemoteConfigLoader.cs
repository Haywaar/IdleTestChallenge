using Configs.CircleConfig;
using Configs.GoldByTapConfig;
using Configs.UpgradeConfig;

namespace Configs.ConfigLoader
{
    public class RemoteConfigLoader : ConfigLoader
    {
        private UpgradeConfigRemote _upgradeConfig = new();
        private CircleConfigRemote _circleConfig = new();
        private GoldByTapConfigRemote _goldByTapConfig = new();

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
            await _upgradeConfig.LoadData("UpgradeConfig.json");
            await _circleConfig.LoadData("CircleConfig.json");
            await _goldByTapConfig.LoadData("GoldByTapConfig.json");
            LoadFinished();
        }
    }
}