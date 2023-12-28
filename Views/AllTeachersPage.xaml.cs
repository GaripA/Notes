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
            
            // Attacher l'événement Appearing
            this.Appearing += AllTeachersPage_Appearing;
        }

        private void AllTeachersPage_Appearing(object sender, EventArgs e)
        {
            // Rechargez la liste des enseignants chaque fois que la page apparaît
            ((AllTeachers)BindingContext).LoadTeachers();
        }

        private async void AddTeacher_Clicked(object sender, EventArgs e)
        {
            // Naviguer vers la page TeacherPage
            await Navigation.PushAsync(new TeacherPage());
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