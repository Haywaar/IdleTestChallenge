using Configs.CircleConfig;
using Configs.GoldByTapConfig;
using Configs.UpgradeConfig;
using UnityEngine;

namespace Configs.ConfigLoader
{
    public class LocalConfigLoader : ConfigLoader
    {
        [SerializeField] private UpgradeConfigScriptableObject _upgradeConfig;
        [SerializeField] private CircleConfigScriptableObject _circleConfig;
        [SerializeField] private GoldByTapConfigScriptableObject _goldByTapConfig;

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

        protected override void LoadAll()
        {
            LoadFinished();
        }
    }
}