// CursePage.xaml.cs
using System;
using Notes.Models;
using Microsoft.Maui.Controls;

namespace Notes.Views
{
    public partial class CursePage : ContentPage
    {
        private AllCurses allCurses;

        public CursePage()
        {
            InitializeComponent();
            allCurses = new AllCurses();
            BindingContext = allCurses;
        }

        private async void Enregistrer_Clicked(object sender, EventArgs e)
        {
            // Récupérer le nom de l'activité depuis le champ de saisie
            string curseName = txtCurseName.Text;

            // Validation des données
            if (string.IsNullOrWhiteSpace(curseName))
            {
                await DisplayAlert("Erreur", "Veuillez entrer le nom du cours.", "OK");
                return;
            }

            // Créer un objet Activity avec le nom de l'activité saisie
            var newCurse = new Curse
            {
                CurseName = curseName
            };

            // Ajouter l'activité à la liste
            allCurses.Curses.Add(newCurse);

            // Réinitialiser le champ de saisie si nécessaire
            txtCurseName.Text = string.Empty;

            // Enregistrer les activités dans le fichier
            allCurses.SaveCurses();

            // Revenir à la page précédente (AllActivitiesPage)
            await Navigation.PopAsync();
        }
    }
}