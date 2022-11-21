using UnityEngine;

namespace Configs
{
    public abstract class GoldByTapConfig : ScriptableObject
    {
        public abstract NumberData.NumberData GetGoldByTapValue(int level);
    }
}