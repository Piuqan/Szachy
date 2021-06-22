using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;
using Microsoft.EntityFrameworkCore;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(GetChessGames("games.csv"));
            //ImportDataToDB("games.csv");
            Top20LongestGames();
            //WatchedGame nowa = new WatchedGame() { GameID = 139979, Watched = "yes", UserID = 1 };
            //InsertOrUpdate(nowa);

        }
            


        public static void ImportDataToDB(string path)
        {
            List<Chess> ChessGames = GetChessGames(path);

            List<Chess> all = new List<Chess>();
            all.AddRange(ChessGames);
            using (var db = new ChessDBContext())
            {
                db.Chess.AddRange(all);
                db.SaveChanges();
            }
        }
        public static List<Chess> GetChessGames(string path)
        {
            List<Chess> returnValue = null;
            if (File.Exists(path))
            {
                using (var reader = new StreamReader(path))
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    var records = csv.GetRecords<Chess>();
                    returnValue = new List<Chess>();
                    foreach (var f in records)
                    {
                        returnValue.Add(f);
                    }
                }
            }
            return returnValue;
        }
        public static void Top20LongestGames()
        {

            using (var db = new ChessDBContext())
            {
                List<Chess> chessGamesList = db.Chess.OrderByDescending(p => p.turns).ToList();
                var chessGamesListCropped = chessGamesList.ToList().Take(20);
                {
                    foreach (var game in chessGamesListCropped)
                    {
                        Console.WriteLine(game.GameID + "    " + game.white_id + "    " + game.black_id + "    " + game.turns);
                    }
                }
            }
        }
        public static void showWatchedList()
        {

            using (var db = new ChessDBContext())
            {
                List<WatchedGame> chessGamesList = db.WatchedGame.Include(p => p.Chesses).ToList();
                {
                    foreach (var game in chessGamesList)
                    {
                        Console.WriteLine(game.GameID + "    " + game.Chesses.white_id + "    " + game.Chesses.black_id);
                    }
                }
            }
        }



        public static void AddGame(WatchedGame Game)
        {
            using (var db = new ChessDBContext())
            {
                db.WatchedGame.Add(Game);
                db.SaveChanges();
                Console.WriteLine("Zapisano obejrzenie meczu " + Game.GameID);
            }
        }

        public static WatchedGame GetGame(int record)
        {
            if (record != 0)
            {
                using (var db = new ChessDBContext())
                {
                    List<WatchedGame> returnValue;
                    returnValue = db.WatchedGame.Where(p => p.GameID == record).ToList();
                    if (returnValue.Count != 0)
                        return returnValue[0];
                }
            }
            return null;
        }


        public static void UpdateGame(WatchedGame record)
        {
            if (record != null)
            {
                using (var db = new ChessDBContext())
                {
                    db.Update(record);
                    db.SaveChanges();
                    Console.WriteLine("Zaktualizowano obejrzenie meczu " + record.GameID);

                }
            }
        }

        public static void InsertOrUpdate(WatchedGame Game)
        {
            if (Game != null && !string.IsNullOrEmpty(Convert.ToString(Game.GameID)))
            {
                    WatchedGame game = GetGame(Game.GameID);
                    if (game == null)
                    {
                        AddGame(Game);
                    }
                    else
                    {
                        Game.ID = game.ID;
                        UpdateGame(Game);
                    }
            }
        }

    }

}