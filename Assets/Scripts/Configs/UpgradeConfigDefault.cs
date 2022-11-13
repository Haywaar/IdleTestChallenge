using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "UpgradeConfigDefault", menuName = "ScriptableObjects/UpgradeConfigDefault", order = 1)]
    public class UpgradeConfigDefault : UpgradeConfig
    {
        [SerializeField] private float koef1;
        [SerializeField] private float koef2;

        public override NumberData GetUpgradePrice(int level)
        {
            var value = Mathf.Pow((koef1 * koef2), level);
            return NumberData.FromInt(Mathf.FloorToInt(value));
        }
    }
}