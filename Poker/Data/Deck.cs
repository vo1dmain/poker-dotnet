namespace Poker.Data;

internal class Deck
{
	private readonly IDictionary<Suit, ISet<Rank>> _pool = CreatePool();


	public IEnumerable<Card> Cards => EvaluateCards();

	public int Size => EvaluateSize();


	public IEnumerable<Card> RandomCards(int count)
	{
		if (count < 1 || count > Size) throw new ArgumentException("Argument is out of bounds", nameof(count));

		var random = new Random();
		var picked = Cards.OrderBy(it => random.Next()).Take(count).ToList();

		foreach (var card in picked) RemoveFromPool(card.Suit, card.Rank);

		return picked;
	}



	private ISet<Rank> MutableRanksFor(Suit suit) =>
		_pool[suit];

	private IEnumerable<Card> EvaluateCards() =>
		_pool.SelectMany(entry => entry.Value.Select(rank => new Card(entry.Key, rank)));

	private int EvaluateSize() =>
		_pool.Select(it => it.Value.Count).Sum();


	private void RemoveFromPool(Suit suit, Rank rank)
	{
		var ranks = MutableRanksFor(suit);
		ranks.Remove(rank);
		if (ranks.Count == 0) _pool.Remove(suit);
	}

	private static IDictionary<Suit, ISet<Rank>> CreatePool()
	{
		var suits = Enum.GetValues<Suit>();
		var ranks = Enum.GetValues<Rank>();

		var destination = new Dictionary<Suit, ISet<Rank>>();
		foreach (var suit in suits) destination[suit] = ranks.ToHashSet();
		return destination;
	}
}
