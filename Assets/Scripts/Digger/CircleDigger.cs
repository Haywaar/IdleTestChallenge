using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Digger
{
    public class CircleDigger : Digger
    {
        public CircleDigger(int id, int level) : base(id, level)
        {
        }

        public void StartAttack()
        {
            PeriodicAttack(1.0f);
        }

        private async void PeriodicAttack(float cooldown)
        {
            while (true)
            {
                Attack();
                await UniTask.Delay((int)(cooldown * 1000));
            }
        }

        protected override void OnUpgrade(int id, int level)
        {
            base.OnUpgrade(id, level);
            //TODO - play animation
        }
    }
}
