using System.Text.RegularExpressions;

namespace AoC23.Solutions {
    public class SolutionDay02 : Solution {
        public override void SolvePart1() {
            // List<string> input = ReadInputFile("Day02TestInput1");
            List<string> input = ReadInputFile("Day02Input1");
            int result = input.Sum(IsGamePossible);
            Console.WriteLine($"Result: {result}");
        }

        public override void SolvePart2() {
            //List<string> input = ReadInputFile("Day02TestInput1");
            List<string> input = ReadInputFile("Day02Input1");
            int result = input.Sum(CalculatePower);
            Console.WriteLine($"Result: {result}");
        }

        private int CalculatePower(string game) {
            ExtractGameInfo(game, out _, out Dictionary<string, List<int>> colorDrawMapping);
            int gamePower = colorDrawMapping.Aggregate(1, (x, y) => x * y.Value.Max());
            return gamePower;
        }

        private int IsGamePossible(string game) {
            List<Tuple<string, int>> cubeLimits = new() {
                new("red", 12), new("green", 13), new("blue", 14)
            };
            ExtractGameInfo(game, out int result, out Dictionary<string, List<int>> colorDrawMapping);

            if (cubeLimits.Any(limit => colorDrawMapping[limit.Item1].Any(draw => draw > limit.Item2))) {
                result = 0;
            }

            return result;
        }

        private static void ExtractGameInfo(string game, out int gameId, out Dictionary<string, List<int>> colorDrawMapping) {
            Match match = Regex.Match(game, @"Game (?'GameId'\d+):( (?'Draw'.*?)(;|$))+");

            string gameIdString = match.Groups["GameId"].Value;
            gameId = int.Parse(gameIdString);
            colorDrawMapping = new();
            colorDrawMapping["red"] = new List<int>();
            colorDrawMapping["green"] = new List<int>();
            colorDrawMapping["blue"] = new List<int>();

            Group drawGroup = match.Groups["Draw"];
            foreach (Capture drawCapture in drawGroup.Captures) {
                List<string> drawnCubes = drawCapture.Value.Split(",").Select(s => s.Trim()).ToList();
                foreach (string draw in drawnCubes) {
                    string[] split = draw.Split(" ");
                    colorDrawMapping[split[1]].Add(int.Parse(split[0]));
                }
            }
        }
    }
}