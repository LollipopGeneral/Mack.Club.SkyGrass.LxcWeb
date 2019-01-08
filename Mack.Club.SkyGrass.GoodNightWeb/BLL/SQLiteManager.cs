using System.Data.SQLite;

namespace Mack.Club.SkyGrass.GoodNightWeb.BLL
{
    public class SQLiteManager
    {
        public SQLiteDataReader GetDataReader(string dbConnectionStr, string sql)
        {
            SQLiteDataReader reader = null;

            SQLiteConnection m_dbConnection = new SQLiteConnection(dbConnectionStr);

            m_dbConnection.Open();

            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection);

            reader = command.ExecuteReader();     

            return reader;
        }
    }
}