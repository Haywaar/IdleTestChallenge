using System.Collections;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject.Signals;

namespace Digger
{
    public class CircleDigger : Digger
    {
        private readonly float _cooldown;
        public CircleDigger(int id, int level, float cooldown) : base(id, level)
        {
            _cooldown = cooldown;
        }

        public void StartAttack()
        {
            PeriodicAttack(_cooldown);
        }

        private async void PeriodicAttack(float cooldown)
        {
            while (true)
            {
                Attack();
                await UniTask.Delay((int)(cooldown * 1000));
            }
        }
    }
}
