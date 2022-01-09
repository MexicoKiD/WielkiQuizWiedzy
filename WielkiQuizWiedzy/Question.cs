namespace WielkiQuizWiedzy
{
    public class Questions
    {
        public int Id {  
            get;  
            set;  
        }  
        public string Question {  
            get;  
            set;  
        }  
        public string AnswerA {  
            get;  
            set;  
        }  
        public string AnswerB {  
            get;  
            set;  
        }  
        public string AnswerC {  
            get;  
            set;  
        }  
        public string AnswerD {  
            get;  
            set;  
        }  
        public string CorrectAnswer {  
            get;  
            set;  
        }  
        public Questions(int Id, string Question, string AnswerA, string AnswerB, string AnswerC, string AnswerD, string CorrectAnswer) {  
            this.Id = Id;  
            this.Question = Question;  
            this.AnswerA = AnswerA;  
            this.AnswerB = AnswerB;  
            this.AnswerC = AnswerC;  
            this.AnswerD = AnswerD;  
            this.CorrectAnswer = CorrectAnswer;  
        }  
        
    }
}