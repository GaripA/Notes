using System;
using Notes.Models;
using Microsoft.Maui.Controls;

namespace Notes.Views
{
    public partial class AllTeachersPage : ContentPage
    {
        private AllTeachers allTeachers;

        public AllTeachersPage()
        {
            InitializeComponent();
            allTeachers = new AllTeachers();
            BindingContext = allTeachers;

            // Attacher l'événement Appearing
            this.Appearing += AllTeachersPage_Appearing;
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
            if (sender is Button button && button.CommandParameter is Teacher teacher)
            {
                // Naviguer vers la page ActivitiesForTeacher en passant l'ID du professeur
                await Navigation.PushAsync(new ActivitiesForTeacher(teacher.TeacherId));
            }
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
    }
}