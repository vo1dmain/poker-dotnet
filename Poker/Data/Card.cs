namespace Poker.Data
{
    internal record struct Card(Suit Suit, Rank Rank) : IComparable<Card>
    {
        public int CompareTo(Card other) => Rank - other.Rank;
    }
}
