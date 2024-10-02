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
                Person person = new Person(dataReader["name"].ToString(), dataReader["guid"].ToString());
                Persons.Add(person);
            }
            Database.connection.Close();
            int randomId = GenerateRandomNumber(Persons.Count());
            for (int i = 0; i < Persons.Count(); i++)
            {
                Persons[i].Id = randomId;
                MappingId[randomId] = i;
                if (++randomId == Persons.Count())
                {
                    randomId = 0;
                }
            }
        }

        private int GenerateRandomNumber(int max)
        {
            Random r = new Random(Guid.NewGuid().GetHashCode());
            return r.Next(1, max);
        }

        internal List<string> Start(double count)
        {
            List<string> names = new List<string>();
            Dictionary<int, bool> rolled = new Dictionary<int, bool>();
            for (int i = 0; i < count; i++)
            {
                int randonNumber = GenerateRandomNumber(Persons.Count());
                if (!rolled.ContainsKey(randonNumber) || rolled[randonNumber] != true)
                {
                    names.Add(Persons[MappingId[randonNumber]].Name);
                    rolled[randonNumber] = true;
                }
                else
                {
                    --i;
                }
            }
            return names;
        }
    }
}
