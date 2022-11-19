using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "UpgradeConfigDefault", menuName = "ScriptableObjects/UpgradeConfigDefault", order = 1)]
    public class UpgradeConfigDefault : UpgradeConfig
    {
        [Header("Formula = (k1 * k2)^level")]
        [SerializeField] private float koef1 = 5;
        [SerializeField] private float koef2 = 1.08f;

        public override NumberData GetUpgradePrice(int level)
        {
            var value = Mathf.Pow((koef1 * koef2), level);
            return NumberData.FromInt(Mathf.FloorToInt(value));
        }
    }
}