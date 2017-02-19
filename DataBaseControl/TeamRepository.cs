using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseControl
{
    class TeamRepository
    {
        private readonly string _connectionString
            = ConfigurationManager.ConnectionStrings["Control"].ConnectionString;

        public IEnumerable<Match> FindMatch()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                List<Match> matches = new List<Match>();
                connection.Open();

                command.CommandText = @"Select m.Id, m.TeamA, A.Name, m.TeamB, B.Name, p.Id, p.TeamId, p.Name, m.TeamAScore, m.TeamBScore from Matches m
                                      JOIN Teams A On m.TeamA = A.Id
                                      JOIN Teams B On m.TeamB = B.Id
                                      JOIN GemaPlayers gp on gp.MatchId = m.Id
                                      JOIN Players p on gp.PlayerId=p.Id 
                                      Where m.TeamA is not null and m.TeamB is not null and p.TeamId is not null and  B.Name is not null 
                                      and A.Name is not null and p.Name is not null";
                using (
                var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Match match = new Match
                        {
                            MatchId = reader.GetInt32(0),
                            TeamAId = reader.GetInt32(1),
                            TeamAName = reader.GetString(2),
                            TeamBId = reader.GetInt32(3),
                            TeamBName = reader.GetString(4),
                            Players = new List<Player>(),
                            TeamAScore = reader.GetInt32(8),
                            TeamBScore = reader.GetInt32(9)
                        };
                        matches.Add(match);

                        if (match.TeamAId == reader.GetInt32(6) || match.TeamBId == reader.GetInt32(6))
                        {
                            Player player = new Player
                            {
                                Id = reader.GetInt32(5),
                                TeamId = reader.GetInt32(6),
                                Name = reader.GetString(7)
                            };
                            match.Players.Add(player);
                        }
                    }
                    return matches;
                }
            }
        }

        public void CreateMinuteForGoal()
        {
            Random rand = new Random();

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"ALTER TABLE Players ADD GoalTime INT NULL";
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }

            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                using (SqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = $"INSERT INTO Players (GoalTime) VALUES ('{rand.Next(0, 90)}') ";
                    command.ExecuteNonQuery();
                }
                connection.Close();
            }
            for (int i = 1; i < 151; i += 3)
            {
                int goalTime = rand.Next(0, 90);
            }
        }

        public IEnumerable<Match> FindMatchWithMinutes()
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            using (SqlCommand command = connection.CreateCommand())
            {
                List<Match> matches = new List<Match>();
                connection.Open();

                command.CommandText = @"SELECT m.Id, m.TeamA, A.Name, m.TeamB, B.Name, p.Id, p.TeamId, p.Name, m.TeamAScore, m.TeamBScore,
                                        p.GoalTime FROM Matches m
	                                    JOIN Teams A On m.TeamA = A.Id
                                        JOIN Teams B On m.TeamB = B.Id
	                                    JOIN GemaPlayers gp on gp.MatchId = m.Id
                                        JOIN Players p on gp.PlayerId=p.Id
	                                    Where m.TeamA is not null and m.TeamB is not null and p.TeamId is not null and  B.Name is not null 
                                        and A.Name is not null and p.Name is not null and p.GoalTime is not null";


                using (
                var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Match match = new Match
                        {
                            MatchId = reader.GetInt32(0),
                            TeamAId = reader.GetInt32(1),
                            TeamAName = reader.GetString(2),
                            TeamBId = reader.GetInt32(3),
                            TeamBName = reader.GetString(4),
                            Players = new List<Player>(),
                            TeamAScore = reader.GetInt32(8),
                            TeamBScore = reader.GetInt32(9)
                        };
                        matches.Add(match);

                        Player player = new Player
                        {
                            Id = reader.GetInt32(5),
                            TeamId = reader.GetInt32(6),
                            Name = reader.GetString(7),
                            GoalTime = reader.GetInt32(10)
                        };

                        match.Players.Add(player);
                    }
                    return matches;
                }
            }
        }
    }
}
