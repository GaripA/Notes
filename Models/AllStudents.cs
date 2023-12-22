// AllStudents.cs
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace Notes.Models
{
    public class AllStudents
    {
        public ObservableCollection<Student> Students { get; set; }

        public AllStudents()
        {
            // Load students from file or create a new collection
            Students = LoadStudents() ?? new ObservableCollection<Student>();
        }

        public void SaveStudents()
        {
            string json = JsonSerializer.Serialize(Students);
            File.WriteAllText("students.json", json);
        }

        private ObservableCollection<Student> LoadStudents()
        {
            if (File.Exists("students.json"))
            {
                string json = File.ReadAllText("students.json");
                return JsonSerializer.Deserialize<ObservableCollection<Student>>(json);
            }
            return null;
        }
    }
}