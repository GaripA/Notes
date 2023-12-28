using System;
using Notes.Models;
using Microsoft.Maui.Controls;

namespace Notes.Views
{
    public partial class AllStudentsPage : ContentPage
    {
        private AllStudents allStudents;

        public AllStudentsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Load students every time the page appears
            allStudents = new AllStudents();
            BindingContext = allStudents;
        }

        private async void AddStudent_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new StudentPage());
        }

        private void Supprimer_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Student student)
            {
                // Remove the student from the list
                allStudents.Students.Remove(student);

                // Save students to the file
                allStudents.SaveStudents();
            }
        }
    }
}