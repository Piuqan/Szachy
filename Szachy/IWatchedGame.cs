namespace Chess
{
    public interface IWatchedGame
    {
        int GameID { get; set; }
        int ID { get; set; }
        int timesWatched { get; set; }
        string Watched { get; set; }
    }
}