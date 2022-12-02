namespace AoC2022._20221202
{
    internal static class RockPaperScissors
    {
        public static void Run()
        {
            var input = File.ReadAllLines("20221202/input.txt")
                .Select(s => (ToNum(s[0]), ToNum(s[2])));

            Console.WriteLine($"points sum: {input.CountPoints()}");

            Console.WriteLine($"points sum for result: {input.CountPointsForResult()}");
        }

        public static int ToNum(char shape) => shape switch
        {
            'A' or 'X' => 1,
            'B' or 'Y' => 2,
            'C' or 'Z' => 3,
        };

        public static int CountPoints(this IEnumerable<(int left, int right)> input) => input.Sum(el => el switch
        {
            (int l, int r) when l == r => r + 3,
            (int l, int r) when (l == 1 && r == 2) || (l == 2 && r == 3) || (l == 3 && r == 1) => r + 6,
            (int _, int r) => r
        });

        public static int CountPointsForResult(this IEnumerable<(int left, int result)> input) => input.Sum(el => el switch
        {
            (int l, int r) when r is 1 => l == 1 ? 3 : (l == 2 ? 1 : 2),
            (int l, int r) when r is 2 => 3 + l,
            (int l, int) => 6 + (l == 1 ? 2 : l == 2 ? 3 : 1)
        });
    }
}
