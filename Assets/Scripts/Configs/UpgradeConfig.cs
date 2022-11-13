using UnityEngine;

namespace Configs
{
    public abstract class UpgradeConfig : ScriptableObject
    {
        public abstract NumberData GetUpgradePrice(int level);
    }
}
