using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using BloodDonationSystem.DAL.Data;
using BloodDonationSystem.Models.Entities;

namespace BloodDonationSystem.UI
{
    public partial class QuestionManagement : Window
    {
        private readonly BloodDonationDbContext _context = new();

        public QuestionManagement()
        {
            InitializeComponent();
            LoadQuestions();
        }

        private void LoadQuestions()
        {
            QuestionList.Children.Clear();

            var questions = _context.Questions.ToList();

            foreach (var q in questions)
            {
                var border = new Border
                {
                    Margin = new Thickness(0, 5, 0, 5),
                    Padding = new Thickness(10),
                    Background = Brushes.White,
                    BorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#1976D2")),
                    BorderThickness = new Thickness(1),
                    CornerRadius = new CornerRadius(8)
                };

                var stack = new StackPanel { Orientation = Orientation.Horizontal, VerticalAlignment = VerticalAlignment.Center };

                var content = new TextBlock
                {
                    Text = $"📝 {q.Content}",
                    FontSize = 16,
                    Width = 500,
                    Foreground = Brushes.Black,
                    VerticalAlignment = VerticalAlignment.Center
                };
                stack.Children.Add(content);

                // Edit button
                var editBtn = new Button
                {
                    Content = "✏️",
                    Width = 35,
                    Height = 30,
                    Margin = new Thickness(10, 0, 5, 0),
                    Background = Brushes.Orange,
                    Foreground = Brushes.White,
                    Tag = q.Id
                };
                editBtn.Click += Edit_Click;
                stack.Children.Add(editBtn);

                // Delete button
                var deleteBtn = new Button
                {
                    Content = "🗑️",
                    Width = 35,
                    Height = 30,
                    Background = Brushes.Red,
                    Foreground = Brushes.White,
                    Tag = q.Id
                };
                deleteBtn.Click += Delete_Click;
                stack.Children.Add(deleteBtn);

                border.Child = stack;
                QuestionList.Children.Add(border);
            }
        }

        private void AddQuestion_Click(object sender, RoutedEventArgs e)
        {
            var addWindow = new AddQuestionWindow();
            bool? result = addWindow.ShowDialog();

            if (result == true)
            {
                string content = addWindow.QuestionContent;

                var newQuestion = new Question
                {
                    Content = content
                };

                _context.Questions.Add(newQuestion);
                _context.SaveChanges();

                LoadQuestions();
            }
        }


        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
            {
                var question = _context.Questions.Find(id);
                if (question != null)
                {
                    var input = Microsoft.VisualBasic.Interaction.InputBox("Chỉnh sửa nội dung câu hỏi:", "Edit Question", question.Content);
                    if (!string.IsNullOrWhiteSpace(input))
                    {
                        question.Content = input;
                        _context.SaveChanges();
                        LoadQuestions();
                    }
                }
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
            {
                var result = MessageBox.Show("Bạn có chắc muốn xoá câu hỏi này?", "Xác nhận", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    var question = _context.Questions.Find(id);
                    if (question != null)
                    {
                        _context.Questions.Remove(question);
                        _context.SaveChanges();
                        LoadQuestions();
                    }
                }
            }
        }
    }
}
