using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using CsvHelper.Configuration.Attributes;
using System.Collections.Generic;

namespace Chess
{
    [Table("Chess")]
    public class Chess : IChess
    {
        [Ignore]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity), Key]
        public int ID { get; set; }
        public string GameID { get; set; }

        public string rated { get; set; }

        public string created_at { get; set; }

        public string last_move_at { get; set; }

        public int turns { get; set; }

        public string victory_status { get; set; }

        public string winner { get; set; }

        public string increment_code { get; set; }

        public string white_id { get; set; }

        public int white_rating { get; set; }

        public string black_id { get; set; }

        public int black_rating { get; set; }

        public string moves { get; set; }

        public string opening_eco { get; set; }

        public string opening_name { get; set; }

        public int opening_ply { get; set; }

        public virtual List<WatchedGame> WatchedGame { get; set; }
        public virtual List<UserScore> Scores { get; set; }

    }
}
