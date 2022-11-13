using System.Collections;
using UnityEngine;

namespace Digger
{
    public class CircleDigger : Digger
    {
        public CircleDigger(int id, int level) : base(id, level)
        {
        }

        private void Start()
        {
            StartCoroutine(PeriodicAttackCoroutine(1.0f));
        }

        private IEnumerator PeriodicAttackCoroutine(float cooldown)
        {
            while (true)
            {
                Attack();
                yield return new WaitForSeconds(cooldown);
            }
        }

        protected override void OnUpgrade(int id, int level)
        {
            base.OnUpgrade(id, level);
            //TODO - play animation
        }
    }
}
