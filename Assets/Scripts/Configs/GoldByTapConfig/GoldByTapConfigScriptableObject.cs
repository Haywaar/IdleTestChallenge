using UnityEngine;

namespace Configs.GoldByTapConfig
{
    [CreateAssetMenu(fileName = "GoldByTapConfig", menuName = "ScriptableObjects/GoldByTapConfig", order = 1)]
    public class GoldByTapConfigScriptableObject : ScriptableObject, IGoldByTapConfig
    {
        [Header("Formula = k1 * (level^k2)")] [SerializeField]
        private GoldByTapData _configData;

        public NumberData GetGoldByTapValue(int level)
        {
            var value = _configData.koef1 * Mathf.Pow(level, _configData.koef2);
            return NumberData.FromInt(Mathf.FloorToInt(value));
        }
    }
}