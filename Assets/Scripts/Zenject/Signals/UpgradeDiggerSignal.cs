namespace Zenject.Signals
{
   public class UpgradeDiggerSignal
   {
      public readonly int Id;
      public readonly int Level;

      public UpgradeDiggerSignal(int id, int level)
      {
         Id = id;
         Level = level;
      }
   }
}
