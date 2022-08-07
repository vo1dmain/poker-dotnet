namespace Poker.Data;

internal record class Card(Suit Suit, Rank Rank) : IComparable<Card>
{
	public int CompareTo(Card? other)
	{
		if (other == null) return 1;
		return Rank - other.Rank;
	}
}
