namespace Zenject.Signals
{
    public class MoneyChangedSignal
    {
        public readonly NumberData Value;

        public MoneyChangedSignal(NumberData value)
        {
            Value = value;
        }
    }
}