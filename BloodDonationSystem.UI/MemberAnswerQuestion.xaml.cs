using BloodDonationSystem.DAL.Data;
using BloodDonationSystem.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace BloodDonationSystem.UI
{
    public partial class MemberAnswerQuestion : Window
    {
        private readonly BloodDonationDbContext _context = new();
        private readonly Guid _requestId;
        private readonly Guid _memberId;
        private List<Question> _questions = new();
        private Dictionary<int, TextBox> _answerBoxes = new();

        public MemberAnswerQuestion(Guid requestId, Guid memberId)
        {
            InitializeComponent();
            _requestId = requestId;
            _memberId = memberId;
            LoadQuestions();
        }

        private void LoadQuestions()
        {
            _questions = _context.Questions
                        .ToList();

            foreach (var q in _questions)
            {
                var panel = new StackPanel { Margin = new Thickness(0, 10, 0, 10) };

                var text = new TextBlock
                {
                    Text = $"❓ {q.Content}",
                    FontSize = 16,
                    FontWeight = FontWeights.SemiBold,
                    Foreground = System.Windows.Media.Brushes.DarkSlateGray,
                    TextWrapping = TextWrapping.Wrap
                };

                var input = new TextBox
                {
                    Height = 60,
                    AcceptsReturn = true,
                    TextWrapping = TextWrapping.Wrap,
                    VerticalScrollBarVisibility = ScrollBarVisibility.Auto,
                    FontSize = 14,
                    Margin = new Thickness(0, 5, 0, 0),
                    Tag = q.Id
                };

                _answerBoxes[q.Id] = input;

                panel.Children.Add(text);
                panel.Children.Add(input);
                QuestionContainer.Children.Add(panel);
            }
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var newAppointmentId = Guid.NewGuid();

                var answers = new List<Answer>();
                foreach (var control in QuestionContainer.Children)
                {
                    if (control is StackPanel panel &&
                        panel.Children[0] is TextBlock tb &&
                        panel.Children[1] is TextBox input)
                    {
                        string content = input.Text.Trim();
                        if (!string.IsNullOrEmpty(content))
                        {
                            int questionId = (int)input.Tag;

                            answers.Add(new Answer
                            {
                                AppointmentId = newAppointmentId,
                                QuestionId = questionId,
                                Content = content
                            });
                        }
                    }
                }

                if (answers.Count == 0)
                {
                    MessageBox.Show("❗ Bạn chưa trả lời bất kỳ câu hỏi nào.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var appointment = new Appointment
                {
                    Id = newAppointmentId,
                    RequestId = _requestId,
                    MemberId = _memberId,
                    StatusId = 1, // Review
                    Answers = answers
                };

                _context.Appointments.Add(appointment);
                _context.SaveChanges();

                MessageBox.Show("✅ Đăng ký hiến máu thành công! Đang chờ duyệt.", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("❌ Lỗi khi tạo lịch hẹn: " + ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
