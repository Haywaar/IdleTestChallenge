using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "GoldByTapConfig", menuName = "ScriptableObjects/GoldByTapConfig", order = 1)]
    public class GoldByTapConfigDefault : GoldByTapConfig
    {
        [Header("Formula = k1 * (level^k2)")]
        [SerializeField] private float koef1 = 5;
        [SerializeField] private float koef2 = 2.1f;
        
        public override NumberData.NumberData GetGoldByTapValue(int level)
        {
            var value = koef1 * Mathf.Pow(level, koef2);
            return NumberData.NumberData.FromInt(Mathf.FloorToInt(value));
        }
    }
}
