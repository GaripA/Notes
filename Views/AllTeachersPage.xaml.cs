using System;
using System.Linq;
using Notes.Models;
using Microsoft.Maui.Controls;

namespace Notes.Views
{
    public partial class AllTeachersPage : ContentPage
    {
        private AllTeachers allTeachers;
        private AllActivities allActivities; // Ajout de cette ligne

        public AllTeachersPage()
        {
            InitializeComponent();
            allTeachers = new AllTeachers();
            allActivities = new AllActivities(); // Instanciation de AllActivities
            BindingContext = allTeachers;
        }

        private void AllTeachersPage_Appearing(object sender, EventArgs e)
        {
            // Rechargez la liste des enseignants chaque fois que la page apparaît
            allTeachers.LoadTeachers();
        }

        private async void AddTeacher_Clicked(object sender, EventArgs e)
        {
            // Naviguer vers la page TeacherPage
            await Navigation.PushAsync(new TeacherPage());
        }

        private async void LinkToActivity_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ActivitiesForTeacher());
        }

        private void Supprimer_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Teacher teacher)
            {
                // Supprimer l'enseignant de la liste
                allTeachers.Teachers.Remove(teacher);

                // Enregistrer les enseignants dans le fichier
                allTeachers.SaveTeachers();
            }
        }

        private async void Details_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Teacher teacher)
            {
                // Afficher les détails de l'activité associée au professeur
                var activity = allActivities.Activities.FirstOrDefault(a => a.ActivityId == teacher.AssociatedActivityId);
                if (activity != null)
                {
                    await DisplayAlert("Détails de l'activité", $"Activité associée : {activity.ActivityName}", "OK");
                }
                else
                {
                    await DisplayAlert("Détails de l'activité", "Aucune activité associée", "OK");
                }
            }
        }
    }
}
