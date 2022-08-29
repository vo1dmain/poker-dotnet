namespace Poker;

using Poker.Data;

internal class Game
{
    private readonly Deck deck = new();

    public void Start()
    {
        var playersCount = ReadInputUntil("Введите количество игроков: ", it => it >= 2, 0);

        var players = InitializePlayers(playersCount);

        var winner = players.MaxBy(p => p.Hand.MaxBy(c => c.Rank))!;

        var winnerIndex = Array.IndexOf(players, winner);
        var winnerCards = string.Join(", ", winner.Hand);

        Console.WriteLine($"Победил игрок №{winnerIndex + 1} с картами {winnerCards}!");
        PrintIndexed(players);
    }


    private Player[] InitializePlayers(int playersCount)
    {
        var players = new Player[playersCount];

        for (var i = 0; i < players.Length; i++)
        {
            var cards = deck.RandomCards(Player.handSize).ToList();

            players[i] = new Player(cards);
        }

        return players;
    }


    private static int ReadInputUntil(string message, Func<int, bool> condition, int inputDelta = -1)
    {
        int input;
        while (true)
        {
            try
            {
                Console.Write(message);

                var inputLine = Console.ReadLine() ?? throw new IOException();
                input = Convert.ToInt32(inputLine);

                if (!condition(input)) throw new ArgumentException(nameof(input));

                Console.WriteLine();
                break;
            }
            catch (Exception)
            {
                Console.WriteLine("Неправильный ввод. Повторите попытку.");
            }
        }

        return input + inputDelta;
    }

    private static void PrintIndexed<T>(IEnumerable<T> values)
    {
        for (var i = 0; i < values.Count(); i++)
            Console.WriteLine($"{i + 1}) {values.ElementAt(i)}");
    }
}
