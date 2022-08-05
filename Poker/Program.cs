﻿using Poker.Data;

namespace Poker
{
    internal class Program
    {
        static void Main()
        {
            var deck = new Deck();
            var players = new Player[2];

            for (var i = 0; i < players.Length; i++)
            {
                var newPlayer = new Player();

                for (var j = 0; j < newPlayer.Hand.Length; j++)
                {
                    Console.WriteLine($"Игрок №{i + 1} – карта №{j + 1}:");

                    newPlayer.Hand[j] = PickCardFrom(deck);
                }

                players[i] = newPlayer;
            }

            var winner = players.MaxBy(p => p.Hand.MaxBy(c => c.Rank))!;
            var winnerIndex = Array.IndexOf(players, winner);
            var winnerCards = string.Join(", ", winner.Hand);

            Console.WriteLine($"Победил игрок №{winnerIndex + 1} с картами {winnerCards}!");
        }

        private static Card PickCardFrom(Deck deck)
        {
            var suits = deck.Suits;
            PrintIndexed(suits);

            var suitIndex = ReadInputUntil("Выберите масть: ", it => it > 0 && it <= suits.Count());
            var suit = suits.ElementAt(suitIndex);

            Console.WriteLine($"Доступные ранги для масти \"{suit}\":");
            var ranks = deck.GetRanksFor(suit);
            PrintIndexed(ranks);

            var rankIndex = ReadInputUntil("Выберите ранг: ", it => it > 0 && it <= ranks.Count());
            var rank = ranks.ElementAt(rankIndex);

            return deck.TakeCard(suit, rank);
        }

        private static int ReadInputUntil(string message, Func<int, bool> condition)
        {
            int input;
            while (true)
            {
                try
                {
                    Console.Write(message);

                    var inputLine = Console.ReadLine() ?? throw new IOException();
                    input = Convert.ToInt32(inputLine);

                    if (!condition.Invoke(input)) throw new ArgumentException(nameof(input));

                    Console.WriteLine();
                    break;
                }
                catch (Exception)
                {
                    Console.WriteLine("Неправильный ввод. Повторите попытку.");
                }
            }

            return input - 1;
        }

        private static void PrintIndexed<T>(IEnumerable<T> values)
        {
            for (var i = 0; i < values.Count(); i++)
                Console.WriteLine($"{i + 1}) {values.ElementAt(i)}");
        }
    }
}