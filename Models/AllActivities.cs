// AllActivities.cs
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Notes.Models
{
    internal class AllActivities
    {
        public ObservableCollection<Activity> Activities { get; set; }

        public AllActivities()
        {
            // Charge les activités existantes ou crée une nouvelle collection
            Activities = LoadActivities() ?? new ObservableCollection<Activity>();
        }

        private string GetFilePath()
        {
            string appDataPath = FileSystem.AppDataDirectory;
            return Path.Combine(appDataPath, "activities.json");
        }

        public ObservableCollection<Activity> LoadActivities()
        {
            string filePath = GetFilePath();

            try
            {
                if (File.Exists(filePath))
                {
                    string jsonContent = File.ReadAllText(filePath);
                    return JsonSerializer.Deserialize<ObservableCollection<Activity>>(jsonContent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la lecture du fichier : {ex}");
            }

            return null;
        }

        public void SaveActivities()
        {
            string filePath = GetFilePath();
            string jsonContent = JsonSerializer.Serialize(Activities);

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

        public void AddActivity(Activity activity)
        {
            Activities.Add(activity);
            SaveActivities();
        }

        public void RemoveActivity(Activity activity)
        {
            Activities.Remove(activity);
            SaveActivities();
        }

        
    }
}
