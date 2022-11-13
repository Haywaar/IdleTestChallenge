using System;
using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(fileName = "CircleConfig", menuName = "ScriptableObjects/CircleConfig", order = 1)]
    public class CircleConfig : ScriptableObject
    {
        [SerializeField] private int _maxCirclesCount = 5;

        [SerializeField] private int _firstCirclePrice = 100;

        /// <summary>
        /// if first circle costs 100 money, next will be cost 100 * _nextBuyCircleCoef
        /// </summary>
        [SerializeField] private float _nextBuyCircleCoef = 10;

        [SerializeField] private float _circleAttackCooldown;

        public int MaxCirclesCount => _maxCirclesCount;

        public int FirstCirclePrice => _firstCirclePrice;

        public float NextBuyCircleCoef => _nextBuyCircleCoef;

        public float CircleAttackCooldown => _circleAttackCooldown;

        public NumberData GetBuyPrice(int circlesCount)
        {
            var value = _firstCirclePrice * Math.Pow(_nextBuyCircleCoef, circlesCount);
            return NumberData.FromInt(Mathf.FloorToInt((float)value));
        }
    }
}