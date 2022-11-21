using System;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CircleConfig", menuName = "ScriptableObjects/CircleConfig", order = 1)]
    public class CircleConfigScriptableObject : ScriptableObject, ICircleConfig
    {
        [SerializeField] private CircleConfigData _configData;

        public NumberData.NumberData GetBuyPrice(int circlesCount)
        {
            var value = _configData.firstCirclePrice * Math.Pow(_configData.nextBuyCircleCoef, circlesCount);
            return NumberData.NumberData.FromInt(Mathf.FloorToInt((float)value));
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