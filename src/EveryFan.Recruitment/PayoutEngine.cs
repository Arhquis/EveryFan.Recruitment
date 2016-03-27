using EveryFan.Recruitment.Factory;
using EveryFan.Recruitment.PayoutCalculators;
using System.Collections.Generic;

namespace EveryFan.Recruitment
{
    public class PayoutEngine
    {
        private ICalculatorFactory _calculatorFactory;

        public PayoutEngine(ICalculatorFactory calculatorFactory)
        {
            _calculatorFactory = calculatorFactory;
        }

        public IReadOnlyList<TournamentPayout> Calculate(Tournament tournament)
        {
            IPayoutCalculator calculator = _calculatorFactory.CreateCalculator(tournament.PayoutScheme);

            return calculator.Calculate(tournament);
        }
    }
}
