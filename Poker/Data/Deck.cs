namespace Poker.Data
{
    internal class Deck
    {
        private Dictionary<Suit, ISet<Rank>> _pool = CreatePool();


        public IEnumerable<Suit> Suits => _pool.Keys;



        public Card TakeCard(Suit suit, Rank rank)
        {
            RemoveFromPool(suit, rank);
            return new Card(suit, rank);
        }

        public IEnumerable<Rank> GetRanksFor(Suit suit) => GetMutableRanksFor(suit);



        private ISet<Rank> GetMutableRanksFor(Suit suit) => _pool[suit];

        private void RemoveFromPool(Suit suit, Rank rank)
        {
            var ranks = GetMutableRanksFor(suit);
            ranks.Remove(rank);
            if (ranks.Count == 0) _pool.Remove(suit);
        }

        private static Dictionary<Suit, ISet<Rank>> CreatePool()
        {
            var suits = Enum.GetValues<Suit>();
            var ranks = Enum.GetValues<Rank>();

            var destination = new Dictionary<Suit, ISet<Rank>>();
            foreach (var suit in suits) destination[suit] = ranks.ToHashSet();
            return destination;
        }
    }
}
