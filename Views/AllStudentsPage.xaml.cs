// AllStudentsPage.xaml.cs
using System;
using Notes.Models;
using Microsoft.Maui.Controls;

namespace Notes.Views
{
    public partial class AllStudentsPage : ContentPage
    {
        public AllStudentsPage()
        {
            InitializeComponent();
            BindingContext = new AllStudents();
        }

       

        private async void AddStudent_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(StudentPage));
        }

        private void Supprimer_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Student student)
            {
                // Remove the student from the list
                ((AllStudents)BindingContext).Students.Remove(student);

                // Save students to the file
                ((AllStudents)BindingContext).SaveStudents();
            }
        }
    }
}