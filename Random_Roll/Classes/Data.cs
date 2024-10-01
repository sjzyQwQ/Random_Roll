using Microsoft.Data.Sqlite;

namespace Random_Roll.Classes
{
    internal class Database
    {
        readonly internal static SqliteConnection connection = new SqliteConnection("Data Source=Database.db");

        // 创建数据库
        internal static void CreateDatabase()
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(@"
                CREATE TABLE IF NOT EXISTS person
                (
                    name TEXT,
                    guid TEXT
                );
                ", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        // 添加姓名
        internal static void NewPerson(string name)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(@"
                INSERT INTO person (name,guid)
                VALUES ($name ,$guid);
                ", connection);
            command.Parameters.AddWithValue("$name", name);
            command.Parameters.AddWithValue("$guid", Guid.NewGuid().ToString());
            command.ExecuteNonQuery();
            connection.Close();
        }

        // 删除姓名
        internal static void DeletePerson(string guid)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(@"DELETE FROM person WHERE guid=$guid", connection);
            command.Parameters.AddWithValue("$guid", guid);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
}
