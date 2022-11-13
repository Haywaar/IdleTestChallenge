using UnityEngine;

namespace Configs
{
    public class GoldByTapConfigDefault : GoldByTapConfig
    {
        [SerializeField] private float koef1;
        [SerializeField] private float koef2;
        
        public override NumberData GetGoldByTapValue(int level)
        {
            var value = koef1 * Mathf.Pow(level, koef2);
            return NumberData.FromInt(Mathf.FloorToInt(value));
        }
    }
}
