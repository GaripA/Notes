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

        public ActivitiesForTeacher()
        {
            InitializeComponent();
            
            allActivities = new AllActivities();
            allTeachers = new AllTeachers();
            BindingContext = allActivities;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

           
            allActivities = new AllActivities();
            BindingContext = allActivities;
           
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

                // Revenir à la page précédente (AllTeachersPage)
                await Navigation.PopAsync();
            }
        }




        private void OnCollectionViewSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Activity selected)
            {
                selectedActivity = selected;
            }
        }
    }
}
