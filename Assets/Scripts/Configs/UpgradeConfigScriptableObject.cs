using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "UpgradeConfigScriptableObject", menuName = "ScriptableObjects/UpgradeConfigScriptableObject", order = 1)]
    public class UpgradeConfigScriptableObject : ScriptableObject, IUpgradeConfig
    {
        [Header("Formula = (k1 * k2)^level")]
        [SerializeField] private float koef1 = 5;
        [SerializeField] private float koef2 = 1.08f;

        public NumberData GetUpgradePrice(int level)
        {
            var value = Mathf.Pow((koef1 * koef2), level);
            return NumberData.FromInt(Mathf.FloorToInt(value));
        }
    }
}