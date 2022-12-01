namespace AoC2022._20221201
{
    internal static class CalorieCounting
    {
        public static void Run()
        {
            var input = File.ReadAllLines("20221201/input.txt")
                .ParseInput()
                .Select(cal => cal.Sum());

            Console.WriteLine("max value: " + input.Max());
            Console.WriteLine("sum of max 3 values: " + input.OrderByDescending(m => m).Take(3).Sum());
        }

        public static IEnumerable<IEnumerable<int>> ParseInput(this IEnumerable<string> input)
        {
            var result = new List<List<int>>
            {
                new List<int>()
            };

            var current = result[0];

            foreach (var line in input)
            {
                if(string.IsNullOrWhiteSpace(line))
                {
                    current = new List<int>();
                    result.Add(current);
                } 
                else
                {
                    current.Add(int.Parse(line));
                }
            }

            return result;
        }
    }
}
