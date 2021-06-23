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


            //TopXLongestGames(10);
            //WatchedGame nowa = new WatchedGame() { GameID = 139979, Watched = "yes", timesWatched = 1 };
            //InsertOrUpdate(nowa);
            //WatchedGame nowa1 = new WatchedGame() { GameID = 159606, Watched = "yes" };
            //AddWatchedTime(nowa1);
            //showGames("mate", 1000, 2000, "TRUE");
            //longestOpenings(10);
            //gamesOfPlayer("thepawnsofwrath");
            averageRating();
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
        public static void averageRating()
        {

            using (var db = new ChessDBContext())
            {
                List<Chess> chessGamesList = db.Chess.ToList();

                {
                    float avg = 0;
                    foreach (var game in chessGamesList)
                    {
                       avg += (game.white_rating + game.black_rating);
                    }
                    Console.WriteLine("Średne ELO graczy wynosi: " + avg / (chessGamesList.Count() * 2));
                }
            }
        }
        //wypisuje wszystkie gry danego gracza
        public static void gamesOfPlayer(string playerName)
        {

            using (var db = new ChessDBContext())
            {
                List<Chess> chessGamesList = db.Chess.Where(p => p.white_id == playerName || p.black_id == playerName).OrderByDescending(p => p.GameID).ToList();

                {
                    foreach (var game in chessGamesList)
                    {
                        Console.WriteLine(game.GameID + "    " + game.white_id + "    " + game.black_id + "    " + game.victory_status + " " + game.winner);
                    }
                }
            }
        }
        //Wyświetla najdłuższe otwarcia gier
        public static void longestOpenings(int numberOfGames)
        {

            using (var db = new ChessDBContext())
            {
                List<Chess> chessGamesList = db.Chess.OrderByDescending(p => p.opening_ply).ToList();
                var chessGamesListCropped = chessGamesList.ToList().Take(numberOfGames);
                {
                    foreach (var game in chessGamesListCropped)
                    {
                        Console.WriteLine(game.GameID + "    " + game.white_id + "    " + game.black_id + "    " + game.opening_name + " " +game.opening_ply);
                    }
                }
            }
        }
        //Wypisuje X najdłuższych gier pod względem wykonanych ruchów
        public static void TopXLongestGames(int numberOfGames)
        {

            using (var db = new ChessDBContext())
            {
                List<Chess> chessGamesList = db.Chess.OrderByDescending(p => p.turns).ToList();
                var chessGamesListCropped = chessGamesList.ToList().Take(numberOfGames);
                {
                    foreach (var game in chessGamesListCropped)
                    {
                        Console.WriteLine(game.GameID + "    " + game.white_id + "    " + game.black_id + "    " + game.turns);
                    }
                }
            }
        }

        //wypisuje gry które spełniają wymagania
        public static void showGames(string victoryStatus, int minimumRating, int maximumRating, string isRated)
        {

            using (var db = new ChessDBContext())
            {
                List<Chess> chessGamesList = db.Chess.Where(p => p.victory_status == victoryStatus && p.white_rating > minimumRating && p.black_rating > minimumRating
                                                                && p.white_rating < maximumRating && p.black_rating < maximumRating && p.rated == isRated).OrderBy(p => p.GameID).ToList();
                {
                    foreach (var game in chessGamesList)
                    {
                        Console.WriteLine(game.GameID + "    " + game.white_id + "    " + game.black_id + "    " + game.turns);
                    }
                }
            }
        }
        public static void AddWatchedTime(WatchedGame Game)
        {

            using (var db = new ChessDBContext())
            {
                var game = GetGame(Game.GameID);
                if (game == null)
                {
                    AddGame(Game);
                }    
                else
                {
                    game.timesWatched += 1;
                    UpdateGame(game);
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