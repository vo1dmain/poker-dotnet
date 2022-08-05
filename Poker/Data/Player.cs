namespace Poker.Data
{
    internal class Player
    {
        private readonly Card[] _hand = new Card[2];

        public Card[] Hand => _hand;
    }
}
