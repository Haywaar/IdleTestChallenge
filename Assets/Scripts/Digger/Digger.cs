using UnityEngine;

namespace Digger
{
  public abstract class Digger : MonoBehaviour
  {
    protected int _id;

    protected int _level;

    public int ID => _id;

    public int Level => _level;

    public Digger(int id, int level)
    {
      this._id = id;
      this._level = level;
    }

    public void Attack()
    {
      // send signal Attack(id, level)
    }

    protected virtual void OnUpgrade(int id, int level)
    {
      _level = level;
    }
  }
}
