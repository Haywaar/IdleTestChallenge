namespace Zenject.Signals
{
    public class AddMoneySignal
    {
        public readonly NumberData Value;

        public AddMoneySignal(NumberData value)
        {
            Value = value;
        }
    }
}
