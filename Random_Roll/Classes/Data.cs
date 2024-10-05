using Microsoft.Data.Sqlite;

namespace Random_Roll.Classes
{
    internal class Database
    {
        readonly internal static SqliteConnection connection = new SqliteConnection("Data Source=Database.db");

        // 创建表 person
        internal static void CreateTable_person()
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

        // 创建表 statistic
        internal static void CreateTable_statistic()
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(@"
                CREATE TABLE IF NOT EXISTS statistic
                (
                    guid TEXT,
                    timestamp TEXT
                );
                ", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        // 添加
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

        // 删除
        internal static void DeletePerson(string guid)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(@"DELETE FROM person WHERE guid=$guid", connection);
            command.Parameters.AddWithValue("$guid", guid);
            command.ExecuteNonQuery();
            connection.Close();
        }

        // 查询总数
        internal static int GetCount()
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(@"SELECT COUNT(*) FROM person", Database.connection);
            SqliteDataReader dataReader = command.ExecuteReader();
            int count = 0;
            while (dataReader.Read())
            {
                count = dataReader.GetInt32(0);
            }
            connection.Close();
            return count;
        }

        // 写入统计
        internal static void Record(string guid)
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(@"
                INSERT INTO statistic (guid,timestamp)
                VALUES ($guid,$timestamp);
                ", connection);
            command.Parameters.AddWithValue("$guid", guid);
            command.Parameters.AddWithValue("$timestamp", new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds());
            command.ExecuteNonQuery();
            connection.Close();
        }

        // 清空表 statistic
        internal static void ClearTable_Statistic()
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(@"
                DELETE FROM statistic;
                VACUUM;
                ", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

    internal class Person
    {
        internal int Id { get; set; }
        internal string Name { get; set; }
        internal string Guid { get; set; }

        internal Person(string name, string guid)
        {
            Name = name;
            Guid = guid;
        }
    }
}
