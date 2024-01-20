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
            Students = LoadStudents() ?? new ObservableCollection<Student>();
        }

        private string GetStudentsFilePath()
        {
            // Crée et retourne le chemin du fichier pour les étudiants
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(folderPath, "students.json");
        }

        public ObservableCollection<Student> LoadStudents()
        {
            string filePath = GetStudentsFilePath();

            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    var loadedStudents = JsonSerializer.Deserialize<ObservableCollection<Student>>(json);

                    foreach (var student in loadedStudents)
                    {
                        if (student.AssociatedCourses == null)
                        {
                            student.AssociatedCourses = new List<string>();
                        }
                    }

                    return loadedStudents;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la lecture du fichier : {ex}");
            }

            return null;
        }

        public void SaveStudents()
        {
            string filePath = GetStudentsFilePath();
            string json = JsonSerializer.Serialize(Students);

            try
            {
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'écriture dans le fichier : {ex}");
            }
        }

        public void AssociateStudentAndCourse(Student student, string courseName)
        {
            student.AssociatedCourses.Add(courseName);
            SaveStudents();
        }

        public void AddEvaluation(Student student, string evaluation)
        {
            student.Evaluation = evaluation;
            SaveStudents();
        }
    }
}
