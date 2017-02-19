using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseControl
{
    class Program
    {
        static void Main(string[] args)
        {
            TeamRepository rep = new TeamRepository();
            var matches = rep.FindMatch();

            foreach (var match in matches)
            {
                Console.WriteLine($"{match.TeamAName} - {match.TeamBName} {match.TeamAScore} : {match.TeamBScore}");
                Console.WriteLine();
                Console.WriteLine($"Team {match.TeamAName} players");
                foreach (var player in match.Players.Where(p => p.TeamId == match.TeamAId))
                {
                    Console.WriteLine($"{player.Name}");
                }
                Console.WriteLine();
                Console.WriteLine($"Team {match.TeamBName} players");
                foreach (var player in match.Players.Where(p => p.TeamId == match.TeamBId))
                {
                    Console.WriteLine($"{player.Name}");
                }
                Console.WriteLine("********");
            }

            rep.CreateMinuteForGoal();

            matches = rep.FindMatchWithMinutes();
            foreach (var match in matches)
            {
                Console.WriteLine($"{match.TeamAName} - {match.TeamBName} {match.TeamAScore} : {match.TeamBScore}");
                Console.WriteLine();
                Console.WriteLine($"Team {match.TeamAName} players");
                foreach (var player in match.Players.Where(p => p.TeamId == match.TeamAId))
                {
                    Console.WriteLine($"{player.GoalTime} '' {player.Name}");
                }
                Console.WriteLine();
                Console.WriteLine($"Team {match.TeamBName} players");
                foreach (var player in match.Players.Where(p => p.TeamId == match.TeamBId))
                {
                    Console.WriteLine($"{player.Name}");
                }
                Console.WriteLine("********");
            }
            Console.ReadLine();
        }
    }
}
