namespace Notes;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        

        Routing.RegisterRoute(nameof(Views.AddEvalPage), typeof(Views.AddEvalPage));
        Routing.RegisterRoute(nameof(Views.AllCursesPage), typeof(Views.AllCursesPage));
        Routing.RegisterRoute(nameof(Views.CursePage), typeof(Views.CursePage));
        Routing.RegisterRoute(nameof(Views.AllTeachersPage), typeof(Views.AllTeachersPage));
        Routing.RegisterRoute(nameof(Views.ActivityPage), typeof(Views.ActivityPage));
        Routing.RegisterRoute(nameof(Views.AllActivitiesPage), typeof(Views.AllActivitiesPage));
        Routing.RegisterRoute(nameof(Views.TeacherPage), typeof(Views.TeacherPage));
        Routing.RegisterRoute(nameof(Views.AllTeachersPage), typeof(Views.AllTeachersPage));
        Routing.RegisterRoute(nameof(Views.StudentPage), typeof(Views.StudentPage));
        Routing.RegisterRoute(nameof(Views.AllStudentsPage), typeof(Views.AllStudentsPage));
        
        
    }
}
     