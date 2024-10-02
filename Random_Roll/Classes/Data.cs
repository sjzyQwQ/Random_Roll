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
