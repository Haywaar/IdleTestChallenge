namespace Zenject.Signals
{
    public class AddMoneySignal
    {
        public readonly NumberData.NumberData Value;

        public AddMoneySignal(NumberData.NumberData value)
        {
            Value = value;
        }
    }
}
