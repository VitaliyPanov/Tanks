namespace Tanks.Core.Infrastructure
{
    public sealed class Calculator:IInput
    {
        public int Multiply(int a, int b)
        {
            return a * b;
        }
    }

    public interface IInput
    {
    }
}
