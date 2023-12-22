using System;
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
            // Charge les étudiants existants ou crée une nouvelle collection
            Students = LoadStudents() ?? new ObservableCollection<Student>();
        }

        private string GetFilePath()
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(folderPath, "students.json");
        }

        private ObservableCollection<Student> LoadStudents()
        {
            string filePath = GetFilePath();

            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    return JsonSerializer.Deserialize<ObservableCollection<Student>>(json);
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
            string filePath = GetFilePath();
            string json = JsonSerializer.Serialize(Students);

            try
            {
                // Crée le fichier s'il n'existe pas
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'écriture dans le fichier : {ex}");
            }
        }
        public List<int> GetStudentIds()
        {
            // Retourne une liste d'identifiants uniques pour les étudiants
            return Students.Select(student => student.Id).ToList();
        }   
    }
}