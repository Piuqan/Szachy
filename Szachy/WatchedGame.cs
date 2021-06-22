using CsvHelper.Configuration.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Chess
{
    [Table("Watched")]
    public class WatchedGame
    {
        [Ignore]
        [DatabaseGenerated(System.ComponentModel.DataAnnotations.Schema.DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [ForeignKey("ID")]
        public int GameID { get; set; }

        public string Watched { get; set; }

        public int UserID{ get; set; }

        public virtual Chess Chesses { get; set; }

    }
}
