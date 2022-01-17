using Android.Content;
using Android.Database;
using Android.Database.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;

namespace WielkiQuizWiedzy {
public class DbHelper: SQLiteOpenHelper {
        private static readonly string DbPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        private static readonly string DbName = "MyDB.db";  
        private static readonly int Version = 1;
        private readonly Context _context;
        public DbHelper(Context context): base(context, DbName, null, Version) {
            _context = context;
        }
        private string GetSQLitePath() {    
            return Path.Combine(DbPath, DbName);
        }
        public override SQLiteDatabase WritableDatabase => CreateSqLiteDb();

        private SQLiteDatabase CreateSqLiteDb() {
            SQLiteDatabase sqliteDb = null;
            string path = GetSQLitePath();
            Stream streamSQLite = null;
            FileStream streamWriter = null;
            Boolean isSQLiteInit = false;
            try {
                if (File.Exists(path)) isSQLiteInit = true;
                else {
                    streamSQLite = _context.Resources.OpenRawResource(Resource.Raw.MyDB);
                    streamWriter = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                    if (streamSQLite != null && streamWriter != null) {
                        if (CopySQLiteDB(streamSQLite, streamWriter)) isSQLiteInit = true;
                    }
                }
                if (isSQLiteInit) sqliteDb = SQLiteDatabase.OpenDatabase(path, null, DatabaseOpenFlags.OpenReadwrite);
            } catch {}
            return sqliteDb;
        }
        private bool CopySQLiteDB(Stream streamSQLite, FileStream streamWriter) {
            bool isSuccess = false;
            int length = 1024;
            Byte[] buffer = new Byte[length];
            try {
                int bytesRead = streamSQLite.Read(buffer, 0, length);
                while (bytesRead > 0) {
                    streamWriter.Write(buffer, 0, bytesRead);
                    bytesRead = streamSQLite.Read(buffer, 0, length);
                }
                isSuccess = true; 
            } catch {} finally {
                streamWriter.Close();
                streamSQLite.Close(); 
            }
            return isSuccess;
        }
        public override void OnCreate(SQLiteDatabase db) {}
        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion) {}  
        public void InsertScore(double score) {
            String query = $"INSERT INTO Ranking(Score) VALUES({score})";
            SQLiteDatabase db = WritableDatabase;
            db.ExecSQL(query);
        }
        public List < Ranking > GetRanking() {
            List < Ranking > lstRanking = new List < Ranking > ();
            SQLiteDatabase db = WritableDatabase;
            ICursor c;
            try {
                c = db.RawQuery($"SELECT * FROM Ranking ORDER BY Score desc LIMIT 10", null);
                if (c == null) {
                    return null;
                } else {
                    c.MoveToNext();
                }
                do {
                    int Id = c.GetInt(c.GetColumnIndex("Id"));
                    double Score = c.GetDouble(c.GetColumnIndex("Score")); 
                    Ranking ranking = new Ranking(Id, Score);
                    lstRanking.Add(ranking);
                }
                while (c.MoveToNext());
                c.Close();
            } catch {}
            db.Close();
            return lstRanking;
        }
        public List<Questions> GetQuestionMode(string mode) {
            List <Questions> lstQuestion = new List <Questions> (); 
            SQLiteDatabase db = WritableDatabase;
            ICursor c;
            int limit = 0;
            if (mode.Equals(Commonn.MODE.EASY.ToString())) limit = Commonn.EASY_MODE_NUM;
            else if (mode.Equals(Commonn.MODE.MEDIUM.ToString())) limit = Commonn.MEDIUM_MODE_NUM;
            else if (mode.Equals(Commonn.MODE.HARD.ToString())) limit = Commonn.HARD_MODE_NUM;
            else limit = Commonn.HARDEST_MODE_NUM;
            try {
                c = db.RawQuery($"SELECT * FROM Questions ORDER BY Random() LIMIT {limit}", null);
                if (c == null) return null;
                c.MoveToFirst();
                do {
                    int Id = c.GetInt(c.GetColumnIndex("ID"));
                    string Question = c.GetString(c.GetColumnIndex("Question"));
                    string AnswerA = c.GetString(c.GetColumnIndex("AnswerA"));
                    string AnswerB = c.GetString(c.GetColumnIndex("AnswerB"));
                    string AnswerC = c.GetString(c.GetColumnIndex("AnswerC"));
                    string AnswerD = c.GetString(c.GetColumnIndex("AnswerD"));
                    string CorrectAnswer = c.GetString(c.GetColumnIndex("CorrectAnswer"));
                    Questions question = new Questions(Id, Question, AnswerA, AnswerB, AnswerC, AnswerD, CorrectAnswer);
                    lstQuestion.Add(question);
                }
                while (c.MoveToNext());
                c.Close();
            } catch {}
            return lstQuestion;
        }  
        //Update 2.0
        public int GetPlayCount(int Level) {
            int result = 0;
            SQLiteDatabase db = WritableDatabase;
            ICursor c;
            try {
                c = db.RawQuery($"SELECT PlayCount FROM UserPlayCount WHERE Level={Level}", null);
                if (c == null) return 0;
                c.MoveToFirst();
                do {
                    result = c.GetInt(c.GetColumnIndex("PlayCount"));
                } while (c.MoveToNext());
                c.Close();
            }
            catch (Exception)
            {
                // ignored
            }

            return result;
        }
        public void UpdatePlayCount(int Level, int PlayCount) {
            string query = $"UPDATE UserPlayCount SET PlayCount={PlayCount} WHERE Level={Level}";
            SQLiteDatabase db = WritableDatabase;
            db.ExecSQL(query);
        }
    }
}