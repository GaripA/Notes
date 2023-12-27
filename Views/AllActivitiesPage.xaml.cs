using System;
using Notes.Models;
using Microsoft.Maui.Controls;

namespace Notes.Views
{
    public partial class AllActivitiesPage : ContentPage
    {
        private AllActivities allActivities;

        public AllActivitiesPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Load activities every time the page appears
            allActivities = new AllActivities();
            BindingContext = allActivities;
        }

        private async void AddActivity_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ActivityPage));
        }

        private void Supprimer_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Activity activity)
            {
                // Supprimer l'activité de la liste
                allActivities.Activities.Remove(activity);

                // Enregistrer les activités dans le fichier
                allActivities.SaveActivities();
            }
        }
    }
}