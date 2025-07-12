using BloodDonationSystem.DAL.Data;
using BloodDonationSystem.Models.Entities;
using BloodDonationSystem.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace BloodDonationSystem.UI
{
    public partial class CreateBloodRequest : Window
    {
        private readonly Account _currentStaff;
        private readonly BloodRequestService _service;
        public CreateBloodRequest()
        {
            InitializeComponent();
        }

        public CreateBloodRequest(Account staff)
        {
            InitializeComponent();
            _currentStaff = staff;
            _service = new BloodRequestService(new BloodDonationDbContext());
        }

        private void SubmitRequest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string title = TitleBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(title))
                {
                    return;
                }
                string? priorityText = ((ComboBoxItem)PriorityBox.SelectedItem)?.Content.ToString()?.ToLower();
                int priorityId = priorityText switch
                {
                    "low" => 1,
                    "medium" => 2,
                    "high" => 3,
                    _ => throw new Exception("Priority not selected.")
                };

                int maxPeople = int.TryParse(MaxPeopleBox.Text, out int val) ? val : 1;
                DateTime startDateTime = CombineDateAndTime(StartDatePicker, StartTimeBox.Text);
                DateTime endDateTime = CombineDateAndTime(EndDatePicker, EndTimeBox.Text);
                if (endDateTime <= startDateTime)
                {
                    return;
                }

                List<int> selectedBloodGroupIds = new();
                foreach (ComboBoxItem item in BloodGroupBox.Items)
                {
                    if (item.IsSelected)
                    {
                        string content = item.Content.ToString();
                        int bloodGroupId = content switch
                        {
                            "O+" => 1,
                            "O-" => 2,
                            "A+" => 3,
                            "A-" => 4,
                            "B+" => 5,
                            "B-" => 6,
                            "AB+" => 7,
                            "AB-" => 8,
                            _ => throw new Exception("Invalid blood group.")
                        };
                        selectedBloodGroupIds.Add(bloodGroupId);
                    }
                }

                if (selectedBloodGroupIds.Count == 0)
                {
                    return;
                }
                var requestId = Guid.NewGuid();
                var bloodRequest = new BloodRequest
                {
                    Id = requestId,
                    StaffId = _currentStaff.Id,
                    Title = title,
                    PriorityId = priorityId,
                    MaxPeople = maxPeople,
                    StartTime = startDateTime,
                    EndTime = endDateTime,
                    IsActive = true,
                    CreatedAt = DateTime.Now,
                    RequestBloodGroups = selectedBloodGroupIds.Select(id => new RequestBloodGroup
                    {
                        RequestId = requestId,
                        BloodGroupId = id
                    }).ToList()
                };
                _service.CreateBloodRequest(bloodRequest);
                this.Close();
            }
            catch (Exception ex)
            {
                var error = ex.InnerException?.Message ?? ex.Message;
                MessageBox.Show($"❌ Error: {error}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private DateTime CombineDateAndTime(DatePicker datePicker, string timeText)
        {
            if (datePicker.SelectedDate == null)
                throw new Exception("Date is required.");

            DateTime date = datePicker.SelectedDate.Value;
            if (!DateTime.TryParseExact(timeText, "h:mm tt", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime time))
            {
                throw new Exception("Invalid time format. Use e.g. 10:00 AM");
            }

            return date.Date + time.TimeOfDay;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
