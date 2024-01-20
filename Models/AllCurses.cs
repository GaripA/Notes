// AllCurses.cs
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Notes.Models
{
    internal class AllCurses
    {
        public ObservableCollection<Curse> Curses { get; set; }

        public AllCurses()
        {
            // Charge les activités existantes ou crée une nouvelle collection
            Curses = LoadCurses() ?? new ObservableCollection<Curse>();
        }

        private string GetFilePath()
        {
            string appDataPath = FileSystem.AppDataDirectory;
            return Path.Combine(appDataPath, "curses.json");
        }

        public ObservableCollection<Curse> LoadCurses()
        {
            string filePath = GetFilePath();

            try
            {
                if (File.Exists(filePath))
                {
                    string jsonContent = File.ReadAllText(filePath);
                    return JsonSerializer.Deserialize<ObservableCollection<Curse>>(jsonContent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la lecture du fichier : {ex}");
            }

            return null;
        }

        public void SaveCurses()
        {
            string filePath = GetFilePath();
            string jsonContent = JsonSerializer.Serialize(Curses);

            try
            {
                // Crée le fichier s'il n'existe pas
                File.WriteAllText(filePath, jsonContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'écriture dans le fichier : {ex}");
            }
        }

        public void AddCurse(Curse curse)
        {
            Curses.Add(curse);
            SaveCurses();
        }

        public void RemoveCurse(Curse curse)
        {
            Curses.Remove(curse);
            SaveCurses();
        }

        public void LoadCursesForStudent(int studentId)
        {
            // Filter activities based on the teacher's ID
            var cursesForStudent = Curses.Where(a => a.ResponsibleStudentId == studentId).ToList();

            // Clear and re-add filtered activities to the ObservableCollection
            Curses.Clear();
            foreach (var curse in cursesForStudent)
            {
                Curses.Add(curse);
            }
        }
    }
}
