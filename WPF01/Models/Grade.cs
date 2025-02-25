using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WPF01.Models;

public class Grade : INotifyPropertyChanged
{
    private double value;
    
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int CourseId { get; set; }
    public double Value 
    { 
        get => value;
        set
        {
            this.value = value;
            OnPropertyChanged();
        }
    }
    public DateTime Date { get; set; }
    
    public Student? Student { get; set; }
    public Course? Course { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;
    
    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
} 