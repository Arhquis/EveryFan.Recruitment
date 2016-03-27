using System;
using System.Collections.Generic;
using System.Linq;

namespace EveryFan.Recruitment.PayoutCalculators
{
    /// <summary>
    /// FiftyFifty payout calculator. The 50/50 payout scheme returns double the tournament buyin to people
    /// who finish in the top half of the table. If the number of runners is odd the player in the middle position
    /// should get their stake back. Any tied positions should have the sum of the amount due to those positions
    /// split equally among them.
    /// </summary>
    public class FiftyFiftyPayoutCalculator : PayoutCalculator
    {
        protected override IReadOnlyList<PayingPosition> GetPayingPositions(Tournament tournament)
        {
            throw new NotImplementedException();
        }
    }
}
