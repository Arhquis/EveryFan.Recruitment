using System;
using System.Collections.Generic;
using System.Linq;

namespace EveryFan.Recruitment.PayoutCalculators
{
    /// <summary>
    /// Winner takes all payout calculator, the winner recieves the entire prize pool. In the event of a tie for the winning position the
    /// prize pool is split equally between the tied players.
    /// </summary>
    public class WinnerTakesAllPayoutCalculator : PayoutCalculator
    {
        protected override IReadOnlyList<PayingPosition> GetPayingPositions(Tournament tournament)
        {
            throw new NotImplementedException();
        }
    }
}
