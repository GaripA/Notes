using System;
using Notes.Models;
using Microsoft.Maui.Controls;

namespace Notes.Views
{
    public partial class AllCursesPage : ContentPage
    {
        private AllCurses allCurses;
        private AllStudents allStudents;

        public AllCursesPage()
        {
            InitializeComponent();
            allCurses = new AllCurses();
            allStudents = new AllStudents();
            BindingContext = allCurses;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Load curses every time the page appears
            allCurses = new AllCurses();
            BindingContext = allCurses;
        }

        private async void AddCurse_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CursePage());
        }

        private void Supprimer_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Curse curse)
            {
                // Remove the curse from the list
                allCurses.Curses.Remove(curse);

                // Save curses to the file
                allCurses.SaveCurses();
            }
        }
    }
}