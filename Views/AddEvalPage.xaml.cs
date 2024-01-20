using Microsoft.Maui.Controls;
using Notes.Models;
using System;

namespace Notes.Views
{
    public partial class AddEvalPage : ContentPage
    {
        private AllStudents allStudents;
        private Student selectedStudent;
        private string selectedCurse;

        public AddEvalPage(AllStudents allStudents, Student selectedStudent, string selectedCurse)
        {
            InitializeComponent();
            this.allStudents = allStudents;
            this.selectedStudent = selectedStudent;
            this.selectedCurse = selectedCurse;
        }

        private void Enregistrer_Clicked(object sender, EventArgs e)
        {
            string evaluation = evaluationEntry.Text;

            // Enregistrez l'évaluation pour l'étudiant et le cours sélectionnés
            AddStudentEvaluation(selectedStudent, selectedCurse, evaluation);

            // Retournez à la page AllStudentsPage
            Navigation.PopAsync();
        }

        private void AddStudentEvaluation(Student student, string selectedCurse, string evaluation)
        {
            // Implémenter la logique pour ajouter une évaluation pour l'étudiant dans le cours sélectionné
            // Vous pouvez utiliser une nouvelle propriété ou une structure de données pour stocker les évaluations par cours
            // Par exemple, vous pourriez avoir une classe Evaluation associant un étudiant, un cours et la note.
            // Ensuite, vous mettez à jour cette structure de données et rafraîchissez l'interface utilisateur.

            // Exemple simplifié :
            student.Evaluation = $"{selectedCurse}: {evaluation}";

            // Enregistrez les changements
            allStudents.SaveStudents();

            // Mise à jour de l'interface utilisateur
            // (Notez que dans ce scénario, vous pourriez également choisir de mettre à jour seulement l'item spécifique dans la collection au lieu de tout recharger)
        }
    }
}