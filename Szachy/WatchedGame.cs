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
    [Table("Watched1")]
    public class WatchedGame
    {
        public int ID { get; set; }

        [ForeignKey("ID")]
        public int GameID { get; set; }

        public string Watched { get; set; }

        public int UserID{ get; set; }

        public virtual List<Chess> Games { get; set; }

    }
}
