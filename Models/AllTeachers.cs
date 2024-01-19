// AllTeachers.cs
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Notes.Models
{
    internal class AllTeachers
    {
        public ObservableCollection<Teacher> Teachers { get; set; } = new ObservableCollection<Teacher>();

        public AllTeachers()
        {
            LoadTeachers();
        }

        public void LoadTeachers()
        {
            Teachers.Clear();

            // Get the folder where the teachers are stored.
            string appDataPath = FileSystem.AppDataDirectory;
            string teacherFilePath = Path.Combine(appDataPath, "teacher.txt");

            // Check if the file exists
            if (File.Exists(teacherFilePath))
            {
                // Read the JSON content from the file and deserialize it into a collection of teachers
                string jsonContent = File.ReadAllText(teacherFilePath);
                var teachers = JsonSerializer.Deserialize<ObservableCollection<Teacher>>(jsonContent);

                // Add each teacher into the ObservableCollection
                foreach (Teacher teacher in teachers)
                    Teachers.Add(teacher);
            }
        }

        public void SaveTeachers()
        {
            // Get the folder where the teachers will be stored.
            string appDataPath = FileSystem.AppDataDirectory;
            string teacherFilePath = Path.Combine(appDataPath, "teacher.txt");

            // Serialize the ObservableCollection of teachers to JSON
            string jsonContent = JsonSerializer.Serialize(Teachers);

            // Write the JSON content to the file
            File.WriteAllText(teacherFilePath, jsonContent);
        }

        public void AssociateTeacherAndActivity(Teacher teacher, string activityName)
        {
            // Créez une nouvelle instance de Teacher avec les mêmes propriétés que l'enseignant passé en paramètre
            var updatedTeacher = new Teacher
            {
                TeacherId = teacher.TeacherId,
                Nom = teacher.Nom,
                Prenom = teacher.Prenom,
                AssociatedActivity = activityName
            };

            // Mettez à jour l'enseignant dans la collection
            int index = Teachers.IndexOf(teacher);
            Teachers[index] = updatedTeacher;

            // Enregistrez les changements
            SaveTeachers();
        }
    }
}

