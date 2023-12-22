﻿namespace Notes;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(Views.NotePage), typeof(Views.NotePage));
        Routing.RegisterRoute(nameof(Views.TeacherPage), typeof(Views.TeacherPage));
        Routing.RegisterRoute(nameof(Views.AllTeachersPage), typeof(Views.AllTeachersPage));
        Routing.RegisterRoute(nameof(Views.StudentPage), typeof(Views.StudentPage));
        Routing.RegisterRoute(nameof(Views.AllStudentsPage), typeof(Views.AllStudentsPage));
        
        
    }
}
