namespace Zenject.Signals
{
    public class SpendMoneySignal
    {
        public readonly NumberData Value;

        public SpendMoneySignal(NumberData value)
        {
            Value = value;
        }
    }
}
