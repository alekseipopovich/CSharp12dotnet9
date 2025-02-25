using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF01.Models;

public class Course : INotifyPropertyChanged
{
    private string name = string.Empty;
    
    public int Id { get; set; }
    public string Name 
    { 
        get => name;
        set
        {
            name = value;
            OnPropertyChanged();
        }
    }
    public string Description { get; set; } = string.Empty;
    public List<Student> Students { get; set; } = new();

    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
} 