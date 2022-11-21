namespace Zenject.Signals
{
    public class SpendMoneySignal
    {
        public readonly NumberData.NumberData Value;

        public SpendMoneySignal(NumberData.NumberData value)
        {
            Value = value;
        }
    }
}
