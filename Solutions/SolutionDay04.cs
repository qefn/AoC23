using System.Text.RegularExpressions;

namespace AoC23.Solutions {
    public class SolutionDay04 : Solution {
        public override void SolvePart1() {
            int result = 0;
            // List<string> input = ReadInputFile("Day04TestInput1");
            List<string> input = ReadInputFile("Day04Input1");
            foreach (string game in input) {
                Match match = Regex.Match(game, @":(( )+(?'WinningNumbers'\d+))+ \|(( )+(?'LotNumbers'\d+))+");
                List<int> winningNumbers = match.Groups["WinningNumbers"].Captures.Select(c => int.Parse(c.Value)).ToList();
                List<int> lotNumbers = match.Groups["LotNumbers"].Captures.Select(c => int.Parse(c.Value)).ToList();
                int matches = winningNumbers.Count(win => lotNumbers.Any(lot => lot == win));
                result += matches == 0 ? 0 : Convert.ToInt32(Math.Pow(2, matches - 1));
            }
            Console.WriteLine($"Total points: {result}");
        }

        public override void SolvePart2() {
            // List<string> input = ReadInputFile("Day04TestInput1");
            List<string> input = ReadInputFile("Day04Input1");
            List<ScratchCard> cards = input.Select(game => new ScratchCard(game)).ToList();
            for (int i = 0; i < cards.Count; i++) {
                ScratchCard card = cards[i];
                if (card.Matches == 0) {
                    continue;
                }
                List<ScratchCard> wonCards = new();
                for (int j = 1; j <= card.Matches; j++) {
                    wonCards.Add(cards.First(c => c.GameId == card.GameId + j).Copy());
                }
                cards.AddRange(wonCards);
            }

            Console.WriteLine($"Total points: {cards.Count}");
        }

        private class ScratchCard {
            public ScratchCard(string cardString) {
                Match match = Regex.Match(cardString, @"(?'GameId'\d+):(( )+(?'WinningNumbers'\d+))+ \|(( )+(?'LotNumbers'\d+))+");

                GameId = int.Parse(match.Groups["GameId"].Value);
                WinningNumbers = match.Groups["WinningNumbers"].Captures.Select(c => int.Parse(c.Value)).ToList();
                LotNumbers = match.Groups["LotNumbers"].Captures.Select(c => int.Parse(c.Value)).ToList();
            }

            public ScratchCard(int gameId, List<int> winningNumbers, List<int> lotNumbers) {
                GameId = gameId;
                WinningNumbers = winningNumbers;
                LotNumbers = lotNumbers;
            }

            public int GameId { get; }

            public List<int> LotNumbers { get; }

            public int Matches => WinningNumbers.Count(win => LotNumbers.Any(lot => lot == win));

            public List<int> WinningNumbers { get; }

            public ScratchCard Copy() {
                return new(GameId, WinningNumbers, LotNumbers);
            }
        }
    }
}