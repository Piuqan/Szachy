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
        int GameID { get; set; }
        //Rated (T/F)
        bool rated { get; }
        //Start Time
        decimal created_at { get; }
        //End Time
        decimal last_move_at { get; }
        //Number of Turns
        int turns { get; }
        //Game Status
        string victory_status { get; }
        //Winner
        string winner { get; }
        //Time Increment
        string increment_code { get; }
        //White Player ID
        string white_id { get; }
        //White Player Rating
        string white_rating { get; }
        //Black Player ID
        string black_id { get; }
        //Black Player Rating
        string black_rating { get; }
        //All Moves in Standard Chess Notation
        string moves { get; }
        //Opening Eco (Standardised Code) 
        string opening_eco { get; }
        //Opening Name
        string opening_name { get; }
        //Opening Ply(Number of moves in the opening phase)
        int opening_ply { get; }
    }
}
