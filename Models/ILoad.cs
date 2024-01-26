namespace Notes.Models;
using System.Collections.ObjectModel;
public interface ILoad<T>
{
    ObservableCollection<T> Load();
}