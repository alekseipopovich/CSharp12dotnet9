using System.Windows;
using WPF01.Models;

namespace WPF01
{
    public partial class EditStudentWindow : Window
    {
        private Student _student;

        public EditStudentWindow(Student student)
        {
            InitializeComponent();
            _student = student;
            
            FirstNameTextBox.Text = student.FirstName;
            LastNameTextBox.Text = student.LastName;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text) || 
                string.IsNullOrWhiteSpace(LastNameTextBox.Text))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _student.FirstName = FirstNameTextBox.Text;
            _student.LastName = LastNameTextBox.Text;

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
} 