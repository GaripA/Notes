using System;
using Notes.Models;
using Microsoft.Maui.Controls;

namespace Notes.Views
{
    public partial class AllTeachersPage : ContentPage
    {
        public AllTeachersPage()
        {
            InitializeComponent();
            BindingContext = new AllTeachers();
        }

        private async void AddTeacher_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(TeacherPage));
        }

        private void Supprimer_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Teacher teacher)
            {
                // Supprimer l'enseignant de la liste
                ((AllTeachers)BindingContext).Teachers.Remove(teacher);

                // Enregistrer les enseignants dans le fichier
                ((AllTeachers)BindingContext).SaveTeachers();
            }
        }
    }
}