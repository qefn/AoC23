using System.Drawing;

namespace AoC23.Solutions;

public class SolutionDay03 : Solution {
    public override void SolvePart1() {
        // List<string> input = ReadInputFile("Day03Testinput1");
        List<string> input = ReadInputFile("Day03Input1");
        char[][] schematic = GetSchematic(input);
        List<PartNumber> partNumbers = GetPartNumbers(schematic);

        for (int y = 0; y < schematic.Length; y++) {
            char[] line = schematic[y];
            for (int x = 0; x < line.Length; x++) {
                char curChar = line[x];
                if (char.IsDigit(curChar) || curChar == '.') {
                    continue;
                }
                foreach (PartNumber partNumber in partNumbers.Where(p => !p.IsValid && p.Area.Contains(x, y))) { partNumber.IsValid = true; }
            }
        }

        int result = partNumbers.Where(p => p.IsValid).Sum(p => p.Number);
        Console.WriteLine($"Sum of all part numbers: {result}");
    }

    public override void SolvePart2() {
        // List<string> input = ReadInputFile("Day03Testinput1");
        List<string> input = ReadInputFile("Day03Input1");
        char[][] schematic = GetSchematic(input);
        List<PartNumber> partNumbers = GetPartNumbers(schematic);
        List<int> gearRations = new();

        for (int y = 0; y < schematic.Length; y++) {
            char[] line = schematic[y];
            for (int x = 0; x < line.Length; x++) {
                char curChar = line[x];
                if (curChar != '*') {
                    continue;
                }
                List<PartNumber> matchingParts = partNumbers.Where(p => p.Area.Contains(x, y)).ToList();
                if (matchingParts.Count == 2) {
                    gearRations.Add(matchingParts[0].Number * matchingParts[1].Number);
                }
            }
        }

        Console.WriteLine($"Sum of all gear ratios: {gearRations.Sum()}");
    }

    private static List<PartNumber> GetPartNumbers(char[][] schematic) {
        List<PartNumber> partNumbers = new();

        for (int y = 0; y < schematic.Length; y++) {
            char[] line = schematic[y];
            PartNumber? curPartNumber = null;
            for (int x = 0; x < line.Length; x++) {
                char curChar = line[x];
                if (char.IsDigit(curChar)) {
                    if (curPartNumber == null) {
                        curPartNumber = new PartNumber(x, y);
                        partNumbers.Add(curPartNumber);
                    }
                    curPartNumber.NumberString += curChar;
                    if (x == line.Length - 1) {
                        curPartNumber.ToX = x;
                    }
                } else {
                    if (curPartNumber != null) {
                        curPartNumber.ToX = x - 1;
                    }
                    curPartNumber = null;
                }
            }
        }

        return partNumbers;
    }

    private static char[][] GetSchematic(List<string> input) {
        return input.Select(line => line.ToArray()).ToArray();
    }

    private class PartNumber {
        public PartNumber(int fromX, int y) {
            FromX = fromX;
            Y = y;
        }

        public int Number => int.Parse(NumberString);

        public string NumberString { get; set; } = string.Empty;


        public int FromX { get; }

        public int ToX { get; set; }

        public int Y { get; }

        public bool IsValid { get; set; }

        public Rectangle Area => new Rectangle(FromX - 1, Y - 1, ToX - FromX + 3, 3);
    }
}