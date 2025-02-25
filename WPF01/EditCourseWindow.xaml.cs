using System.Windows;
using WPF01.Models;

namespace WPF01
{
    public partial class EditCourseWindow : Window
    {
        private Course _course;

        public EditCourseWindow(Course course)
        {
            InitializeComponent();
            _course = course;
            
            // Заполняем поля текущими значениями
            NameTextBox.Text = course.Name;
            DescriptionTextBox.Text = course.Description;
        }

        private void OK_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NameTextBox.Text))
            {
                MessageBox.Show("Введите название курса!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            _course.Name = NameTextBox.Text;
            _course.Description = DescriptionTextBox.Text;

            DialogResult = true;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
} 