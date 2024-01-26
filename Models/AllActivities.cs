using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace Notes.Models
{
    public class AllActivities : ILoad<Activity>, ISave
    {
        public ObservableCollection<Activity> Activities { get; set; }

        public AllActivities()
        {
            Activities = Load() ?? new ObservableCollection<Activity>();
        }

        private string GetActivitiesFilePath()
        {
            // Crée et retourne le chemin du fichier pour les activités
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(folderPath, "activities.json");
        }

        public ObservableCollection<Activity> Load()
        {
            string filePath = GetActivitiesFilePath();

            try
            {
                if (File.Exists(filePath))
                {
                    string json = File.ReadAllText(filePath);
                    return JsonSerializer.Deserialize<ObservableCollection<Activity>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la lecture du fichier : {ex}");
            }

            return null;
        }

        public void Save()
        {
            string filePath = GetActivitiesFilePath();
            string json = JsonSerializer.Serialize(Activities);

            try
            {
                File.WriteAllText(filePath, json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'écriture dans le fichier : {ex}");
            }
        }

       
    }
}
