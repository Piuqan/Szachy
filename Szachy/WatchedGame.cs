using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations.Schema;

namespace Chess
{
    [Table("Watched")]
    public class WatchedGame : IWatchedGame
    {
        [Ignore]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [ForeignKey("ID")]
        public int GameID { get; set; }

        public string Watched { get; set; }

        public int timesWatched { get; set; }

        public virtual Chess game { get; set; }

    }
}
