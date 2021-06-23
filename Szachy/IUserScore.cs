namespace Chess
{
    public interface IUserScore
    {
        int ID { get; set; }
        int IDGame { get; set; }
        string userID { get; set; }
        int userScore { get; set; }
    }
}