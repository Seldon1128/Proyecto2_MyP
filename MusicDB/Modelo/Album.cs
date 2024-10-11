
namespace MusicDB.Modelo
{
    public class Album
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Year { get; set; }

        public override string ToString()
        {
            return $"{Id}: {Name} ({Year})";
        }
    }
}
