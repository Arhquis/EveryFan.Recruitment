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

            List<int> groupEntries = tournament.Entries.GroupBy(e => e.Chips).Select(g => g.Count()).ToList();

            int prizePool = CalculatePrizePool(tournament.PrizePool, tournament.BuyIn, groupEntries);
            int nrOfWinners = CalculateNumberOfWinners(groupEntries);

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

            // Check if there is a middle man.
            if (tournament.PrizePool != prizePool)
            {
                for (int i = 0; i < groupEntries[groupEntries.Count / 2]; i++)
                {
                    rvPayingPositions.Add(new PayingPosition
                    {
                        Position = rvPayingPositions.Last().Position + 1,
                        Payout = tournament.BuyIn
                    });
                }
            }

            return rvPayingPositions;
        }

        private int CalculatePrizePool(int originalPrizePool, int buyIn, List<int> groupEntries)
        {
            int prizePool = originalPrizePool;

            // The number of competitors are odd.
            if (groupEntries.Count > 2 && groupEntries.Count % 2 != 0)
            {
                prizePool -= (buyIn * groupEntries[groupEntries.Count / 2]);
            }

            return prizePool;
        }

        private int CalculateNumberOfWinners(List<int> groupEntries)
        {
            if (groupEntries.Count == 1)
            {
                return groupEntries[0];
            }

            int nrOfWinners = 0;

            for (int i = 0; i < groupEntries.Count / 2; i++)
            {
                nrOfWinners += groupEntries[i];
            }

            return nrOfWinners;
        }
    }
}
