using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess
{
    interface IChess
    {


        //Game ID
        string GameID { get; set; }
        //Rated (T/F)
        string rated { get; set; }
        //Start Time
        string created_at { get; set; }
        //End Time
        string last_move_at { get; set; }
        //Number of Turns
        int turns { get; set; }
        //Game Status
        string victory_status { get; set; }
        //Winner
        string winner { get; set; }
        //Time Increment
        string increment_code { get; set; }
        //White Player ID
        string white_id { get; set; }
        //White Player Rating
        int white_rating { get; set; }
          //Black Player ID
         string black_id { get; set; }
          //Black Player Rating
         int black_rating { get; set; }
        //All Moves in Standard Chess Notation
        string moves { get; set; }
        //Opening Eco (Standardised Code) 
        string opening_eco { get; set; }
        //Opening Name
        string opening_name { get; set; }
        //Opening Ply(Number of moves in the opening phase)
        int opening_ply { get; set; }
    }
}
