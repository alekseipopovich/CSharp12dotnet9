using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF01.Models;

public class Student : INotifyPropertyChanged
{
    private string lastName = string.Empty;
    
    public int Id { get; set; }
    public string FirstName { get; set; } = string.Empty;
    public string LastName 
    { 
        get => lastName;
        set
        {
            lastName = value;
            OnPropertyChanged();
        }
    }
    public List<Course> Courses { get; set; } = new();
    public List<Grade> Grades { get; set; } = new();

    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
} 