// Student.cs
namespace Notes.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Classe { get; set; }
        public List<string> AssociatedCourses { get; set; } = new List<string>();
        public string Evaluation { get; set; }
    }
}