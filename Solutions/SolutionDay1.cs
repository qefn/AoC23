namespace AoC23.Solutions;
public class SolutionDay01 : Solution {
    public override void SolvePart1() {
        List<string> input = ReadInputFile("Day1Input1");
        int result = input.Select(line => new Tuple<char, char>(line.First(c => char.IsDigit(c)), line.Last(c => char.IsDigit(c)))).Sum(t => int.Parse(t.Item1.ToString() + t.Item2.ToString()));
        Console.WriteLine($"The result is: {result}");
    }

    public override void SolvePart2() {
        Dictionary<int, string> numberTextMap = new();
        numberTextMap.Add(1, "one");
        numberTextMap.Add(2, "two");
        numberTextMap.Add(3, "three");
        numberTextMap.Add(4, "four");
        numberTextMap.Add(5, "five");
        numberTextMap.Add(6, "six");
        numberTextMap.Add(7, "seven");
        numberTextMap.Add(8, "eight");
        numberTextMap.Add(9, "nine");

        List<string> input = ReadInputFile("Day1Input1");
        // List<string> input = ReadInputFile("Day1TestInput2");
        int result = 0;
        for (int i = 0; i < input.Count; i++) {
            string line = input[i];
            List<Tuple<int, int>> firstDigitStrings = numberTextMap.Select(kvp => new Tuple<int, int>(line.IndexOf(kvp.Value), kvp.Key)).ToList();
            List<Tuple<int, int>> lastDigitStrings = numberTextMap.Select(kvp => new Tuple<int, int>(line.LastIndexOf(kvp.Value), kvp.Key)).ToList();

            if (line.Any(c => char.IsDigit(c))) {
                char firstDigitChar = line.First(c => char.IsDigit(c));
                int firstDigitCharPos = line.IndexOf(firstDigitChar);
                firstDigitStrings.Add(new(firstDigitCharPos, int.Parse(firstDigitChar.ToString())));

                char lastDigitChar = line.Last(c => char.IsDigit(c));
                int lastDigitCharPos = line.LastIndexOf(lastDigitChar);
                lastDigitStrings.Add(new(lastDigitCharPos, int.Parse(lastDigitChar.ToString())));
            }
            int first = firstDigitStrings.Where(i => i.Item1 >= 0).OrderBy(t => t.Item1).First().Item2;
            int last = lastDigitStrings.Where(i => i.Item1 >= 0).OrderByDescending(t => t.Item1).First().Item2;

            result += int.Parse(first.ToString() + last.ToString());
        }
        Console.WriteLine($"The result is: {result}");
    }
}
