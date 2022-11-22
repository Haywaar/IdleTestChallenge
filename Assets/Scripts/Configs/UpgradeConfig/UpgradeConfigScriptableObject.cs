using UnityEngine;

namespace Configs.UpgradeConfig
{
    [CreateAssetMenu(fileName = "UpgradeConfigScriptableObject", menuName = "ScriptableObjects/UpgradeConfigScriptableObject", order = 1)]
    public class UpgradeConfigScriptableObject : ScriptableObject, IUpgradeConfig
    {
        [Header("Formula = (k1 * k2)^level")] [SerializeField]
        private UpgradeConfigData _configData;

        public NumberData GetUpgradePrice(int level)
        {
            var value = Mathf.Pow((_configData.koef1 * _configData.koef2), level);
            return NumberData.FromInt(Mathf.FloorToInt(value));
        }
    }
}