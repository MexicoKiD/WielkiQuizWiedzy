using System;
using System.Collections.Generic;
using System.IO;
using Android.Content;
using Android.Database;
using Android.Database.Sqlite;

namespace WielkiQuizWiedzy
{
    public class DbHelper : SQLiteOpenHelper
    {
        private static readonly string DbPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
        private static readonly string DbName = "MyDB.db";
        private static readonly int Version = 1;
        private readonly Context _context;

        public DbHelper(Context context) : base(context, DbName, null, Version)
        {
            _context = context;
        }

        public override SQLiteDatabase WritableDatabase => CreateSqLiteDb();

        private string GetSQLitePath()
        {
            return Path.Combine(DbPath, DbName);
        }

        private SQLiteDatabase CreateSqLiteDb()
        {
            SQLiteDatabase sqliteDb = null;
            var path = GetSQLitePath();
            Stream streamSQLite = null;
            FileStream streamWriter = null;
            var isSQLiteInit = false;
            try
            {
                if (File.Exists(path))
                {
                    isSQLiteInit = true;
                }
                else
                {
                    streamSQLite = _context.Resources.OpenRawResource(Resource.Raw.MyDB);
                    streamWriter = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
                    if (streamSQLite != null && streamWriter != null)
                        if (CopySQLiteDB(streamSQLite, streamWriter))
                            isSQLiteInit = true;
                }

                if (isSQLiteInit) sqliteDb = SQLiteDatabase.OpenDatabase(path, null, DatabaseOpenFlags.OpenReadwrite);
            }
            catch
            {
            }

            return sqliteDb;
        }

        private bool CopySQLiteDB(Stream streamSQLite, FileStream streamWriter)
        {
            var isSuccess = false;
            var length = 1024;
            var buffer = new byte[length];
            try
            {
                var bytesRead = streamSQLite.Read(buffer, 0, length);
                while (bytesRead > 0)
                {
                    streamWriter.Write(buffer, 0, bytesRead);
                    bytesRead = streamSQLite.Read(buffer, 0, length);
                }

                isSuccess = true;
            }
            catch
            {
            }
            finally
            {
                streamWriter.Close();
                streamSQLite.Close();
            }

            return isSuccess;
        }

        public override void OnCreate(SQLiteDatabase db)
        {
        }

        public override void OnUpgrade(SQLiteDatabase db, int oldVersion, int newVersion)
        {
        }

        public void InsertScore(double score)
        {
            var query = $"INSERT INTO Ranking(Score) VALUES({score})";
            var db = WritableDatabase;
            db.ExecSQL(query);
        }

        public List<Ranking> GetRanking()
        {
            var lstRanking = new List<Ranking>();
            var db = WritableDatabase;
            ICursor c;
            try
            {
                c = db.RawQuery("SELECT * FROM Ranking ORDER BY Score desc LIMIT 10", null);
                if (c == null)
                    return null;
                c.MoveToNext();

                do
                {
                    var Id = c.GetInt(c.GetColumnIndex("Id"));
                    var Score = c.GetDouble(c.GetColumnIndex("Score"));
                    var ranking = new Ranking(Id, Score);
                    lstRanking.Add(ranking);
                } while (c.MoveToNext());

                c.Close();
            }
            catch
            {
            }

            db.Close();
            return lstRanking;
        }

        public List<Questions> GetQuestionMode(string mode)
        {
            var lstQuestion = new List<Questions>();
            var db = WritableDatabase;
            ICursor c;
            var limit = 0;
            if (mode.Equals(Common.Mode.ŁATWY.ToString())) limit = Common.EasyModeNum;
            else if (mode.Equals(Common.Mode.ŚREDNI.ToString())) limit = Common.MediumModeNum;
            else if (mode.Equals(Common.Mode.TRUDNY.ToString())) limit = Common.HardModeNum;
            else limit = Common.HardestModeNum;
            try
            {
                c = db.RawQuery($"SELECT * FROM Questions ORDER BY Random() LIMIT {limit}", null);
                if (c == null) return null;
                c.MoveToFirst();
                do
                {
                    var Id = c.GetInt(c.GetColumnIndex("ID"));
                    var Question = c.GetString(c.GetColumnIndex("Question"));
                    var AnswerA = c.GetString(c.GetColumnIndex("AnswerA"));
                    var AnswerB = c.GetString(c.GetColumnIndex("AnswerB"));
                    var AnswerC = c.GetString(c.GetColumnIndex("AnswerC"));
                    var AnswerD = c.GetString(c.GetColumnIndex("AnswerD"));
                    var CorrectAnswer = c.GetString(c.GetColumnIndex("CorrectAnswer"));
                    var question = new Questions(Id, Question, AnswerA, AnswerB, AnswerC, AnswerD, CorrectAnswer);
                    lstQuestion.Add(question);
                } while (c.MoveToNext());

                c.Close();
            }
            catch
            {
            }

            return lstQuestion;
        }

        public int GetPlayCount(int Level)
        {
            var result = 0;
            var db = WritableDatabase;
            ICursor c;
            try
            {
                c = db.RawQuery($"SELECT PlayCount FROM UserPlayCount WHERE Level={Level}", null);
                if (c == null) return 0;
                c.MoveToFirst();
                do
                {
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

        public void UpdatePlayCount(int Level, int PlayCount)
        {
            var query = $"UPDATE UserPlayCount SET PlayCount={PlayCount} WHERE Level={Level}";
            var db = WritableDatabase;
            db.ExecSQL(query);
        }
    }
}