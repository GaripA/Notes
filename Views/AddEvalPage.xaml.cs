using Microsoft.Maui.Controls;
using Notes.Models;
using System;

namespace Notes.Views
{
    public partial class AddEvalPage : ContentPage
    {
        private AllStudents allStudents;
        private Student selectedStudent;
        private string selectedCourse;

        public AddEvalPage(AllStudents allStudents, Student selectedStudent, string selectedCourse)
        {
            InitializeComponent();
            this.allStudents = allStudents;
            this.selectedStudent = selectedStudent;
            this.selectedCourse = selectedCourse;
        }

        private void Enregistrer_Clicked(object sender, EventArgs e)
        {
            string evaluation = evaluationEntry.Text;

            // Enregistrez l'évaluation pour l'étudiant et le cours sélectionnés
            AddStudentEvaluation(selectedStudent, selectedCourse, evaluation);

            // Retournez à la page AllStudentsPage
            Navigation.PopAsync();
        }

        private void AddStudentEvaluation(Student student, string selectedCourse, string evaluation)
        {
            // Implémenter la logique pour ajouter une évaluation pour l'étudiant dans le cours sélectionné
            // Vous pouvez utiliser une nouvelle propriété ou une structure de données pour stocker les évaluations par cours
            // Par exemple, vous pourriez avoir une classe Evaluation associant un étudiant, un cours et la note.
            // Ensuite, vous mettez à jour cette structure de données et rafraîchissez l'interface utilisateur.

            // Exemple simplifié :
            student.Evaluation = $"{selectedCourse}: {evaluation}";

            // Enregistrez les changements
            allStudents.SaveStudents();

            // Mise à jour de l'interface utilisateur
            // (Notez que dans ce scénario, vous pourriez également choisir de mettre à jour seulement l'item spécifique dans la collection au lieu de tout recharger)
        }
    }
}