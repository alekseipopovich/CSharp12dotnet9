using System.Windows;
using WPF01.Models;

namespace WPF01
{
    public partial class AddStudentWindow : Window
    {
        public Student? NewStudent { get; private set; }

        public AddStudentWindow()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(FirstNameTextBox.Text) || 
                string.IsNullOrWhiteSpace(LastNameTextBox.Text))
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NewStudent = new Student
            {
                FirstName = FirstNameTextBox.Text,
                LastName = LastNameTextBox.Text
            };

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
} 