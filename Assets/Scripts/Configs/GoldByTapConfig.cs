using UnityEngine;

namespace Configs
{
    public abstract class GoldByTapConfig : ScriptableObject
    {
        public abstract NumberData GetGoldByTapValue(int level);
    }
}