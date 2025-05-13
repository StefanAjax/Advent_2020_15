using System.Text.RegularExpressions;

namespace Advent_2020_15
{
    internal class Program
    {
        static bool debug = false;
        static void Main(string[] args)
        {
            var data = ParseData("trueData.txt");
            var Game = new Game(data);
            while (Game.TurnNumber <= 30000000)
            {
                Game.PlayTurn();
            }
            Console.WriteLine();
            Console.WriteLine($"Result: {Game.LastNumberSpoken}");
        }

        static List<int> ParseData(string path)
        {
            string input = File.ReadAllText(path);
            var result = new List<int>();
            string pattern = @"\d+";
            var rg = new Regex(pattern);
            MatchCollection matches = rg.Matches(input);
            foreach (Match match in matches) {
                if (match.Success)
                {
                    int val = int.Parse(match.Value);
                    result.Add(val);
                }
            }
            return result;
        }

        class Game
        {
            public Dictionary<int,int> turnNumberOfLastSpoken {  get; set; }
            public int TurnNumber { get; set; }
            public int LastNumberSpoken { get; set; }
            public Game(List<int> puzzleInput)
            {
                TurnNumber = 1;
                turnNumberOfLastSpoken = new Dictionary<int,int>();

                foreach (var number in puzzleInput.SkipLast(1)) {
                    turnNumberOfLastSpoken[number] = TurnNumber;
                    TurnNumber++;
                }
                TurnNumber++;
                LastNumberSpoken = puzzleInput.Last();
            }

            public void PlayTurn()
            {
                //Entering playturn with TurnNumber the current Turn
                if (debug) Console.Write($"Turn: {TurnNumber}, Previous number: {LastNumberSpoken}");
                if (!turnNumberOfLastSpoken.ContainsKey(LastNumberSpoken))
                {
                    // If that was the first time the number has been spoken, the current player says 0
                    if (debug) {Console.WriteLine($", {LastNumberSpoken} was spoken for the first time, speaking 0"); }
                    turnNumberOfLastSpoken[LastNumberSpoken] = TurnNumber-1;
                    LastNumberSpoken = 0;    
                }
                else
                {
                    // Otherwise, the number had been spoken before; the current player announces how many turns apart the number is from when it was previously spoken.
                    // Do something with the two dictionaries
                    
                    int whenItWasLastSpoken = turnNumberOfLastSpoken[LastNumberSpoken];
                    turnNumberOfLastSpoken[LastNumberSpoken] = TurnNumber-1;
                    LastNumberSpoken = (TurnNumber-1) - whenItWasLastSpoken;
                    
                    if (debug) Console.WriteLine($" Speaking: {(TurnNumber - 1)} - {whenItWasLastSpoken} = {LastNumberSpoken}");
                }
                TurnNumber++;
            }
        }
    }
}
