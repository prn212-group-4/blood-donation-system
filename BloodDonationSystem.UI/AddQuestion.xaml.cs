using System.Windows;

namespace BloodDonationSystem.UI
{
    public partial class AddQuestionWindow : Window
    {
        public string QuestionContent { get; private set; } = string.Empty;

        public AddQuestionWindow()
        {
            InitializeComponent();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            string input = QuestionBox.Text.Trim();

            if (string.IsNullOrWhiteSpace(input))
            {
                MessageBox.Show("❌ Content cannot be empty!", "Validation Error",
                    MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            QuestionContent = input;
            DialogResult = true;
            Close();
        }
    }
}
