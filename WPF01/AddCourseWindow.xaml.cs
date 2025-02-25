using System.Windows;
using WPF01.Models;

namespace WPF01
{
    public partial class AddCourseWindow : Window
    {
        public Course? NewCourse { get; private set; }

        public AddCourseWindow()
        {
            InitializeComponent();
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Введите название курса!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            NewCourse = new Course
            {
                Name = NameTextBox.Text,
                Description = DescriptionTextBox.Text
            };

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
} 