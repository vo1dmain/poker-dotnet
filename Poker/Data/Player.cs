namespace Poker.Data;

internal record class Player
{
	public IEnumerable<Card> Hand { get; init; }

	public Player(IEnumerable<Card> hand)
	{
		if (hand.Count() != handSize) throw new ArgumentException($"Hand size should equals {handSize}");
		Hand = hand;
	}

	public override string ToString() => $"Player(Hand = [{string.Join(", ", Hand)}])";


	public const int handSize = 2;
}
