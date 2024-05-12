using Dapper;
using Microsoft.Data.Sqlite;

namespace PING_PONG_API.Domain.Misc
{
    public static class SQLiteInitializer
    {
        public static void InitDb(string connectionString)
        {
            SQLitePCL.Batteries.Init();
            var db = new SqliteConnection(connectionString);
            db.Open();

            db.Execute(@"
            CREATE TABLE IF NOT EXISTS Players (
                Id TEXT PRIMARY KEY, 
                Name TEXT, 
                MatchesWon INTEGER DEFAULT 0, 
                MatchesLost INTEGER DEFAULT 0
            );");

            db.Execute(@"
            CREATE TABLE IF NOT EXISTS Matches (
                Id TEXT PRIMARY KEY, 
                WinnerId TEXT, 
                LoserId TEXT, 
                Score TEXT,
                FOREIGN KEY(WinnerId) REFERENCES Players(Id),
                FOREIGN KEY(LoserId) REFERENCES Players(Id)
            );");

            db.Close();
        }

    }
}
