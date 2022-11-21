namespace Zenject.Signals
{
    public class MoneyChangedSignal
    {
        public readonly NumberData.NumberData Value;

        public MoneyChangedSignal(NumberData.NumberData value)
        {
            Value = value;
        }
    }
}