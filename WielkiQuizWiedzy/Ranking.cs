namespace WielkiQuizWiedzy
{
    public class Ranking
    {
        public int Id {  
            get;  
            set;  
        }  
        public double Score {  
            get;  
            set;  
        }  
        public Ranking(int Id, double Score) {  
            this.Id = Id;  
            this.Score = Score;  
        }
    }
}