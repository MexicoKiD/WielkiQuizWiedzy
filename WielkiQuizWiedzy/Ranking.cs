namespace WielkiQuizWiedzy
{
    public class Ranking
    {
        public Ranking(int Id, double Score)
        {
            this.Id = Id;
            this.Score = Score;
        }

        public int Id { get; set; }
        public double Score { get; set; }
    }
}