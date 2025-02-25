using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WPF01.Models;
using WPF01.Views.Courses;
using WPF01.Views.Students;

namespace WPF01;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public ObservableCollection<Course> Courses { get; set; }
    public ObservableCollection<Student> Students { get; set; }
    public ObservableCollection<Grade> Grades { get; set; }

    public MainWindow()
    {
        // Инициализация коллекций до InitializeComponent
        Courses = new ObservableCollection<Course>();
        Students = new ObservableCollection<Student>();
        Grades = new ObservableCollection<Grade>();

        InitializeComponent();
        
        // Установим DataContext
        DataContext = this;

        // Загрузка тестовых данных
        LoadSampleData();
    }

    private void LoadSampleData()
    {
        // Добавление тестовых курсов
        var course1 = new Course { Id = 1, Name = "Математика", Description = "Базовый курс математики" };
        var course2 = new Course { Id = 2, Name = "Физика", Description = "Базовый курс физики" };
        Courses.Add(course1);
        Courses.Add(course2);

        // Добавление тестовых студентов
        var student1 = new Student { Id = 1, FirstName = "Иван", LastName = "Иванов" };
        var student2 = new Student { Id = 2, FirstName = "Петр", LastName = "Петров" };
        Students.Add(student1);
        Students.Add(student2);

        // Добавление тестовых оценок
        var grade1 = new Grade 
        { 
            Id = 1, 
            StudentId = 1, 
            CourseId = 1, 
            Value = 4.5, 
            Date = DateTime.Now,
            Student = student1,
            Course = course1
        };
        Grades.Add(grade1);
    }

    private void AddCourse_Click(object sender, RoutedEventArgs e)
    {
        var addWindow = new AddCourseWindow();
        if (addWindow.ShowDialog() == true)
        {
            var course = addWindow.NewCourse;
            course.Id = Courses.Count + 1;
            Courses.Add(course);
        }
    }

    private void EditCourse_Click(object sender, RoutedEventArgs e)
    {
        var selectedCourse = CoursesListBox.SelectedItem as Course;
        if (selectedCourse != null)
        {
            var editWindow = new EditCourseWindow(selectedCourse);
            editWindow.ShowDialog();
        }
    }

    private void DeleteCourse_Click(object sender, RoutedEventArgs e)
    {
        var selectedCourse = CoursesListBox.SelectedItem as Course;
        if (selectedCourse != null)
        {
            Courses.Remove(selectedCourse);
        }
    }

    private void AddStudent_Click(object sender, RoutedEventArgs e)
    {
        var addWindow = new AddStudentWindow();
        if (addWindow.ShowDialog() == true)
        {
            var student = addWindow.NewStudent;
            student.Id = Students.Count + 1;
            Students.Add(student);
        }
    }

    private void EditStudent_Click(object sender, RoutedEventArgs e)
    {
        var selectedStudent = StudentsListBox.SelectedItem as Student;
        if (selectedStudent != null)
        {
            var editWindow = new EditStudentWindow(selectedStudent);
            editWindow.ShowDialog();
        }
    }

    private void DeleteStudent_Click(object sender, RoutedEventArgs e)
    {
        var selectedStudent = StudentsListBox.SelectedItem as Student;
        if (selectedStudent != null)
        {
            Students.Remove(selectedStudent);
        }
    }

    private void AddGrade_Click(object sender, RoutedEventArgs e)
    {
        var selectedStudent = StudentsListBox.SelectedItem as Student;
        var selectedCourse = CoursesListBox.SelectedItem as Course;
        
        if (selectedStudent != null && selectedCourse != null)
        {
            var grade = new Grade
            {
                Id = Grades.Count + 1,
                StudentId = selectedStudent.Id,
                CourseId = selectedCourse.Id,
                Student = selectedStudent,
                Course = selectedCourse,
                Value = 4.0,
                Date = DateTime.Now
            };
            Grades.Add(grade);
        }
    }

    private void EditGrade_Click(object sender, RoutedEventArgs e)
    {
        var selectedGrade = GradesDataGrid.SelectedItem as Grade;
        if (selectedGrade != null)
        {
            selectedGrade.Value = Math.Min(5.0, selectedGrade.Value + 0.5);
            GradesDataGrid.Items.Refresh();
        }
    }

    private void DeleteGrade_Click(object sender, RoutedEventArgs e)
    {
        var selectedGrade = GradesDataGrid.SelectedItem as Grade;
        if (selectedGrade != null)
        {
            Grades.Remove(selectedGrade);
        }
    }
}