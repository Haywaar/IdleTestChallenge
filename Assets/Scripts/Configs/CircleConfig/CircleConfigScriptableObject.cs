using System;
using UnityEngine;

namespace Configs.CircleConfig
{
    [CreateAssetMenu(fileName = "CircleConfig", menuName = "ScriptableObjects/CircleConfig", order = 1)]
    public class CircleConfigScriptableObject : ScriptableObject, ICircleConfig
    {
        [Header("BuyPrice = firstCirclePrice*(nextBuyCircleKoef^circlesCount")]
        [SerializeField] private CircleConfigData _configData;

        public NumberData GetBuyPrice(int circlesCount)
        {
            var value = _configData.firstCirclePrice * Math.Pow(_configData.nextBuyCircleKoef, circlesCount);
            return NumberData.FromInt(Mathf.FloorToInt((float)value));
        }

        public int GetMaxCirclesCount()
        {
            return _configData.maxCirclesCount;
        }

        public float GetCircleAttackCooldown()
        {
            return _configData.circleAttackCooldown;
        }
    }
}