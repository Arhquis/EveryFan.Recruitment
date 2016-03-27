using EveryFan.Recruitment.PayoutCalculators;

namespace EveryFan.Recruitment.Factory
{
    public interface ICalculatorFactory
    {
        IPayoutCalculator CreateCalculator(PayoutScheme schema);
    }
}
