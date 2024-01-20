using Microsoft.Maui.Controls;
using Notes.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Notes.Views
{
    public partial class AllStudentsPage : ContentPage
    {
        private AllStudents allStudents;
        private AllCurses allCurses;
        private Student selectedStudent;
        private string selectedCurse;

        public AllStudentsPage()
        {
            InitializeComponent();
            allStudents = new AllStudents();
            allCurses = new AllCurses();
            BindingContext = allStudents;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Recharger les données des cours
            allCurses = new AllCurses();
        }

        private async void DisplayStudentAssociatedCoursesForEvaluation()
        {
            // Filtrer les cours uniquement pour ceux auxquels l'étudiant est associé
            List<Curse> associatedCurses = allCurses.Curses
                .Where(c => selectedStudent.AssociatedCourses.Contains(c.CurseName))
                .ToList();

            selectedCurse = await DisplayActionSheet("Sélectionnez un cours", "Annuler", null, associatedCurses.Select(c => c.CurseName).ToArray());

            if (!string.IsNullOrEmpty(selectedCurse))
            {
                // Appeler la méthode pour ajouter une évaluation pour l'étudiant et le cours sélectionnés
                AddStudentEvaluation(selectedStudent, selectedCurse);
            }
        }

        private void AddStudentEvaluation(Student student, string selectedCurse)
        {
            // Implémenter la logique pour ajouter une évaluation pour l'étudiant dans le cours sélectionné
            // Vous pouvez utiliser une nouvelle propriété ou une structure de données pour stocker les évaluations par cours
            // Par exemple, vous pourriez avoir une classe Evaluation associant un étudiant, un cours et la note.
            // Ensuite, vous mettez à jour cette structure de données et rafraîchissez l'interface utilisateur.

            // Exemple simplifié :
            student.Evaluation = $"{selectedCurse}: En attente d'évaluation";

            // Enregistrez les changements
            allStudents.SaveStudents();

            // Mise à jour de l'interface utilisateur
            UpdateUI();
        }

        private void UpdateUI()
        {
            // Rafraîchir l'affichage des étudiants
            allStudents.LoadStudents();

            // Mise à jour de la liaison des données de la collection
            studentsCollection.ItemsSource = null;
            studentsCollection.ItemsSource = allStudents.Students;

            selectedStudent = null;
        }

        private async void Details_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Student student)
            {
                string details = GetMultipleAssociationDetails(student);
                await DisplayAlert("Détails de l'association", details, "OK");
            }
        }

        private string GetMultipleAssociationDetails(Student student)
        {
            string courses = string.Join(", ", student.AssociatedCourses);
            return $"Nom: {student.Nom}\nPrenom: {student.Prenom}\nCours associés: {courses}\nÉvaluation: {student.Evaluation}";
        }

        private void AfficherCours_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Student student)
            {
                selectedStudent = student;
                DisplayStudentCourses();
            }
        }

        private async void DisplayStudentCourses()
        {
            // Récupérer tous les cours disponibles
            List<Curse> allCursesList = allCurses.Curses.ToList();

            // Afficher une liste des cours pour que l'utilisateur en sélectionne un
            string selectedCurse = await DisplayActionSheet("Sélectionnez un cours", "Annuler", null, allCursesList.Select(c => c.CurseName).ToArray());

            if (!string.IsNullOrEmpty(selectedCurse))
            {
                // Associer l'étudiant sélectionné au cours choisi
                allStudents.AssociateStudentAndCurse(selectedStudent, selectedCurse);

                // Mise à jour de l'interface utilisateur pour refléter l'association
                UpdateUI();
            }
        }

        private async void AddStudent_Clicked(object sender, EventArgs e)
        {
            // Naviguer vers la page StudentPage
            Navigation.PushAsync(new StudentPage());
        }

        private void Supprimer_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Student student)
            {
                // Supprimer l'étudiant de la liste
                allStudents.Students.Remove(student);

                // Enregistrer les étudiants dans le fichier
                allStudents.SaveStudents();
            }
        }

        private void AddEvaluation_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Student student)
            {
                selectedStudent = student;
                Navigation.PushAsync(new AddEvalPage(allStudents, selectedStudent, selectedCurse));
            }
        }

    }
}
