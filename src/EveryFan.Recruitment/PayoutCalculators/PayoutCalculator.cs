using System.Collections.Generic;
using System.Linq;

namespace EveryFan.Recruitment.PayoutCalculators
{
    public abstract class PayoutCalculator : IPayoutCalculator
    {
        protected abstract IReadOnlyList<PayingPosition> GetPayingPositions(Tournament tournament);

        public IReadOnlyList<TournamentPayout> Calculate(Tournament tournament)
        {
            IReadOnlyList<PayingPosition> payingPositions = this.GetPayingPositions(tournament);
            IReadOnlyList<TournamentEntry> orderedEntries = tournament.Entries.OrderByDescending(p => p.Chips).ToList();

            List<TournamentPayout> payouts = new List<TournamentPayout>();
            payouts.AddRange(payingPositions.Select((p, i) => new TournamentPayout()
            {
                Payout = p.Payout,
                UserId = orderedEntries[i].UserId
            }));

            return payouts;
        }
    }
}
