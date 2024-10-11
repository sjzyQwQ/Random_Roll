using Microsoft.Data.Sqlite;

namespace Random_Roll.Classes
{
    internal class Roll
    {
        private List<Person> Persons { get; set; } = new List<Person>();
        private Dictionary<int, int> MappingId { get; set; } = new Dictionary<int, int>();

        internal Roll()
        {
            Database.connection.Open();
            SqliteCommand command = new SqliteCommand(@"SELECT * FROM person", Database.connection);
            var dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                Person person = new Person { Name = dataReader["name"].ToString(), Guid = dataReader["guid"].ToString() };
                Persons.Add(person);
            }
            Database.connection.Close();
            Dictionary<int, bool> usedId = new Dictionary<int, bool>();
            for (int i = 0; i < Persons.Count(); i++)
            {
                int randomId = GenerateRandomNumber(Persons.Count());
                if (!usedId.ContainsKey(randomId) || usedId[randomId] == false)
                {
                    MappingId[randomId] = i;
                    usedId[randomId] = true;
                }
                else
                {
                    --i;
                }
            }
        }

        private int GenerateRandomNumber(int max)
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());
            return r.Next(1, max + 1);
        }

        internal List<Person> Start(double count)
        {
            List<Person> person = new List<Person>();
            Dictionary<int, bool> rolled = new Dictionary<int, bool>();
            for (int i = 0; i < count; i++)
            {
                int randonId = GenerateRandomNumber(Persons.Count());
                if (!rolled.ContainsKey(randonId) || rolled[randonId] == false)
                {
                    person.Add(Persons[MappingId[randonId]]);
                    Database.Record(Persons[MappingId[randonId]].Guid);
                    rolled[randonId] = true;
                }
                else
                {
                    --i;
                }
            }
            return person;
        }
    }
}
