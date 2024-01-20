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
        private AllCourses allCourses;
        private Student selectedStudent;
        private string selectedCourse;

        public AllStudentsPage()
        {
            InitializeComponent();
            allStudents = new AllStudents();
            allCourses = new AllCourses();
            BindingContext = allStudents;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // Recharger les données des cours
            allCourses = new AllCourses();
        }

        private async void DisplayStudentAssociatedCoursesForEvaluation()
        {
            // Filtrer les cours uniquement pour ceux auxquels l'étudiant est associé
            List<Course> associatedCourses = allCourses.Courses
                .Where(c => selectedStudent.AssociatedCourses.Contains(c.CourseName))
                .ToList();

            selectedCourse = await DisplayActionSheet("Sélectionnez un cours", "Annuler", null, associatedCourses.Select(c => c.CourseName).ToArray());

            if (!string.IsNullOrEmpty(selectedCourse))
            {
                // Appeler la méthode pour ajouter une évaluation pour l'étudiant et le cours sélectionnés
                NavigateToAddEvalPage();
            }
        }

        private void NavigateToAddEvalPage()
        {
            if (selectedStudent != null && !string.IsNullOrEmpty(selectedCourse))
            {
                Navigation.PushAsync(new AddEvalPage(allStudents, selectedStudent, selectedCourse));
            }
        }

        private void AddEvaluation_Clicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.CommandParameter is Student student)
            {
                selectedStudent = student;
                DisplayStudentAssociatedCoursesForEvaluation();
            }
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
            List<Course> allCoursesList = allCourses.Courses.ToList();

            // Afficher une liste des cours pour que l'utilisateur en sélectionne un
            string selectedCourse = await DisplayActionSheet("Sélectionnez un cours", "Annuler", null, allCoursesList.Select(c => c.CourseName).ToArray());

            if (!string.IsNullOrEmpty(selectedCourse))
            {
                // Associer l'étudiant sélectionné au cours choisi
                allStudents.AssociateStudentAndCourse(selectedStudent, selectedCourse);

                // Mise à jour de l'interface utilisateur pour refléter l'association
                UpdateUI();
            }
        }

        private async void AddStudent_Clicked(object sender, EventArgs e)
        {
            // Naviguer vers la page StudentPage
            await Navigation.PushAsync(new StudentPage());
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
    }
}
