using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;
using Microsoft.EntityFrameworkCore;

namespace Chess
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine(GetChessGames("games.csv"));
            ImportDataToDB("games.csv");
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
    }
}
