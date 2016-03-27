using EveryFan.Recruitment.PayoutCalculators;
using System;

namespace EveryFan.Recruitment.Factory
{
    public class CalculatorFactory : ICalculatorFactory
    {
        public IPayoutCalculator CreateCalculator(PayoutScheme schema)
        {
            IPayoutCalculator calculator;

            switch (schema)
            {
                case PayoutScheme.FIFTY_FIFY:
                    {
                        calculator = new FiftyFiftyPayoutCalculator();
                        break;
                    }

                case PayoutScheme.WINNER_TAKES_ALL:
                    {
                        calculator = new WinnerTakesAllPayoutCalculator();
                        break;
                    }

                default:
                    {
                        throw new ArgumentOutOfRangeException(nameof(schema));
                    }
            }

            return calculator;
        }
    }
}
