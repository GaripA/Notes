using System;
using Notes.Models;
using Microsoft.Maui.Controls;

namespace Notes.Views
{
    public partial class ActivitiesForTeacher : ContentPage
    {
        private AllActivities allActivities;
        private AllTeachers allTeachers;
        private int teacherId;
        private Activity selectedActivity;

        public ActivitiesForTeacher(int teacherId)
        {
            InitializeComponent();
            this.teacherId = teacherId;
            allActivities = new AllActivities();
            allTeachers = new AllTeachers();
            BindingContext = allActivities;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Load activities for the specific teacher every time the page appears
            allActivities = new AllActivities();
            BindingContext = allActivities;
            allActivities.LoadActivitiesForTeacher(teacherId);
        }

        private async void OnSelectionButtonClicked(object sender, EventArgs e)
        {
            if (selectedActivity != null)
            {
                // Assigner l'enseignant à l'activité sélectionnée
                selectedActivity.ResponsibleTeacherId = teacherId;

                // Enregistrer les activités dans le fichier
                allActivities.SaveActivities();

                // Mettre à jour la propriété AssociatedActivityId de l'enseignant
                var teacher = allTeachers.Teachers.FirstOrDefault(t => t.TeacherId == teacherId);
                if (teacher != null)
                {
                    teacher.AssociatedActivityId = selectedActivity.ActivityId;
                    allTeachers.SaveTeachers();
                }

                // Debugging statement
                Console.WriteLine("Before navigation");

                // Try navigating directly to AllTeachersPage
                await Shell.Current.GoToAsync(nameof(AllTeachersPage));

                // Debugging statement
                Console.WriteLine("After navigation");
            }
        }



        private void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Mettre à jour la propriété selectedActivity lors de la sélection d'un élément dans la collection
            if (e.CurrentSelection.FirstOrDefault() is Activity selected)
            {
                selectedActivity = selected;
            }
        }
    }
}
