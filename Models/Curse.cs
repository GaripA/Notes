namespace Notes.Models
{
    internal class Curse
    {
        public int CurseId { get; set; } // Ajout de cette propriété
        public string CurseName { get; set; }
        public int ResponsibleStudentId { get; set; }
    }
}