using System.Collections.Generic;
using System.Linq;
using Configs;
using UnityEngine;

namespace Digger
{
    public class DiggerManager : MonoBehaviour
    {
        [SerializeField] private CircleConfig _circleConfig;
        [SerializeField] private UpgradeConfig _upgradeConfig;
        [SerializeField] private GoldByTapConfig _goldByTapConfig;
        
        private List<Digger> _diggers = new List<Digger>();

        public NumberData GetUpgradePrice(int diggerId)
        {
            var diggerLevel = _diggers.First(x => x.ID == diggerId).ID;
            return _upgradeConfig.GetUpgradePrice(diggerLevel);
        }

        public void Upgrade(int diggerId)
        {
            //if money is enough
            //send signal money spent 
            // send signal upgradeDigger
        }

        public void BuyCircle()
        {
            var price = GetBuyCirclePrice();
            //if money is enough
            // send signal money spent
            // create new circle
            // add to list
        }

        private NumberData GetBuyCirclePrice()
        {
            // player is digger, so circles = diggers - 1
            int circlesCount = (_diggers.Count - 1);
            return _circleConfig.GetBuyPrice(circlesCount);
        }

        private bool CanBuyCircles()
        {
            return (_diggers.Count - 1) < _circleConfig.MaxCirclesCount;
        }

        private void OnAttack(int id, int level)
        {
            var gold = _goldByTapConfig.GetGoldByTapValue(level);
            // send signal - money received(gold)
        }
    }
}
