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
            List<PayingPosition> rvPayingPositions = new List<PayingPosition>();

            var groupEntries = tournament.Entries.GroupBy(e => e.Chips).Select(g => new
            {
                Count = g.Count()
            }).ToList();

            #region prizepool calculation
            int prizePool = tournament.PrizePool;

            // The number of competitors are odd.
            if (groupEntries.Count() % 2 != 0)
            {
                prizePool -= (tournament.BuyIn * groupEntries[groupEntries.Count / 2].Count);
            }
            #endregion

            #region number of winners calculation
            int nrOfWinners = 0;

            for (int i = 0; i < groupEntries.Count / 2; i++)
            {
                nrOfWinners += groupEntries[i].Count;
            }
            #endregion

            int payoutPerUser = prizePool / nrOfWinners;
            int reminder = prizePool % nrOfWinners;

            for (int i = 0; i < nrOfWinners; i++)
            {
                var position = new PayingPosition
                {
                    Position = i,
                    Payout = payoutPerUser
                };

                if (reminder > 0)
                {
                    position.Payout++;

                    reminder--;
                }

                rvPayingPositions.Add(position);
            }

            #region middle man
            // Check if there is a middle man.
            if (tournament.PrizePool != prizePool)
            {
                for (int i = 0; i < groupEntries[groupEntries.Count / 2].Count; i++)
                {
                    rvPayingPositions.Add(new PayingPosition
                    {
                        Position = rvPayingPositions.Last().Position + 1,
                        Payout = tournament.BuyIn
                    });
                }
            }
            #endregion

            return rvPayingPositions;
        }
    }
}
