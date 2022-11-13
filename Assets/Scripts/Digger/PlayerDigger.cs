using UnityEngine;

namespace Digger
{
    public class PlayerDigger : Digger
    {
        public PlayerDigger(int id, int level) : base(id, level)
        {
        }

        private void Update()
        {
            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                Attack();
            }
        }
    }
}
