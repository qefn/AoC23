
using System.Text.RegularExpressions;

namespace AoC23.Solutions;

public class SolutionDay05 : Solution {
    public override void SolvePart1() {
        // List<string> input = ReadInputFile("Day05TestInput1");
        List<string> input = ReadInputFile("Day05Input1");
        ParseInput(input, false, out List<long> seeds, out Dictionary<string, Almanac> almanacs);
        List<long> locations = new();
        foreach (long seed in seeds) {
            string currentType = "seed";
            long currentValue = seed;

            while (currentType != "location") {
                Almanac almanac = almanacs[currentType];
                currentType = almanac.To;
                currentValue = almanac.FindEntry(currentValue);
            }

            locations.Add(currentValue);
        }
        long lowestLocation = locations.OrderBy(location => location).First();
        Console.WriteLine($"Lowest location: {lowestLocation}");
    }

    public override void SolvePart2() {
        // List<string> input = ReadInputFile("Day05TestInput1");
        List<string> input = ReadInputFile("Day05Input1");
        ParseInput(input, true, out List<long> seeds, out Dictionary<string, Almanac> almanacs);
        List<long> locations = new();
        long lowestLocation = -1;
        object lockObject = new();
        Parallel.ForEach(seeds, seed => {
            string currentType = "seed";
            long currentValue = seed;

            while (currentType != "location") {
                Almanac almanac = almanacs[currentType];
                currentType = almanac.To;
                currentValue = almanac.FindEntry(currentValue);
            }

            lock (lockObject) {
                if (lowestLocation == -1 || currentValue < lowestLocation) {
                    lowestLocation = currentValue;
                    Console.WriteLine(lowestLocation);
                }
            }
        });
        Console.WriteLine($"Lowest location: {lowestLocation}");
    }

    private void ParseInput(List<string> input, bool seedsAreRanges, out List<long> seeds, out Dictionary<string, Almanac> almanacs) {
        seeds = new();
        almanacs = new();
        Almanac? current = null;
        foreach (string line in input) {
            if (line.StartsWith("seeds:")) {
                List<long> seedInputValues = line.Substring("seeds: ".Length).Split(" ").Select(s => long.Parse(s.Trim())).ToList();
                if (seedsAreRanges) {
                    for (int i = 0; i < seedInputValues.Count; i += 2) {
                        for (long j = seedInputValues[i]; j < seedInputValues[i] + seedInputValues[i + 1]; j++) {
                            seeds.Add(j);
                        }
                    }

                } else {
                    seeds.AddRange(seedInputValues);
                }
                continue;
            }

            if (string.IsNullOrEmpty(line)) {
                current = null;
                continue;
            }

            if (!char.IsDigit(line[0])) {
                Match match = Regex.Match(line, @"(?'From'.+)-to-(?'To'.+) ");
                string fromValue = match.Groups["From"].Value;
                current = new(fromValue, match.Groups["To"].Value);
                almanacs.Add(fromValue, current);
                continue;
            }

            long[] numbers = line.Split(" ").Select(s => long.Parse(s.Trim())).ToArray();
            current.Entries.Add(new(numbers[1], numbers[0], numbers[2]));
        }
    }

    private class Almanac {
        public Almanac(string from, string to) {
            From = from;
            To = to;
        }
        public string From { get; }

        public string To { get; }

        public List<AlmanacEntry> Entries { get; } = new();

        public long FindEntry(long toFind) {
            if (!Entries.Any(entry => entry.Matches(toFind))) {
                return toFind;
            }
            AlmanacEntry almanacEntry = Entries.First(entry => entry.Matches(toFind));
            long diff = toFind - almanacEntry.FromStart;
            return almanacEntry.ToStart + diff;
        }
    }

    private class AlmanacEntry {
        public AlmanacEntry(long fromStart, long toStart, long range) {
            FromStart = fromStart;
            ToStart = toStart;
            Range = range;
        }

        public long FromStart { get; }

        long FromEnd => FromStart + Range - 1;

        public long Range { get; }

        public long ToStart { get; }

        internal bool Matches(long toFind) {
            return toFind >= FromStart && toFind <= FromEnd;
        }
    }
}