using System;
using Notes.Models;
using Microsoft.Maui.Controls;

namespace Notes.Views
{
    public partial class ActivitiesForTeacher : ContentPage
    {
        private AllActivities allActivities;
        private int teacherId;
        private Activity selectedActivity;

        public ActivitiesForTeacher(int teacherId)
        {
            InitializeComponent();
            this.teacherId = teacherId;
            allActivities = new AllActivities();
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

        private void OnSelectionButtonClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Activity selectedActivity)
            {
                // Assigner l'enseignant à l'activité sélectionnée
                selectedActivity.ResponsibleTeacherId = teacherId;

                // Enregistrer les activités dans le fichier
                allActivities.SaveActivities();

                // Naviguer vers la page AllTeachersPage
                Shell.Current.GoToAsync(nameof(AllTeachersPage));
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