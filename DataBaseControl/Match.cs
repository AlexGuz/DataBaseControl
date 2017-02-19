using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBaseControl
{
    class Match
    {
        public int MatchId { get; set; }        
        public int TeamAId { get; set; }
        public string TeamAName { get; set; }
        public int TeamBId { get; set; }
        public string TeamBName { get; set; }
        public List<Player> Players { get; set; }
        public int TeamAScore { get; set; }
        public int TeamBScore { get; set; }
    }
}