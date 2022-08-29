namespace Poker.Data;

internal record class Card(Suit Suit, Rank Rank) : IComparable<Card>
{
    public int CompareTo(Card? other) => other is null ? 1 : Rank - other.Rank;
}
