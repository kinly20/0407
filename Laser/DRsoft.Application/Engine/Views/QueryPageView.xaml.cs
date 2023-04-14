using Engine.ViewModels;
using MaterialDesignThemes.Wpf;
using System.Text.RegularExpressions;

namespace Engine.Views
{
    /// <summary>
    /// Interaction logic for QueryPageView.xaml
    /// </summary>
    public partial class QueryPageView : UserControl
    {
        public QueryPageView()
        {
            InitializeComponent();
        }

        public void StartCombinedDialogOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {
            StartCombinedCalendar.SelectedDate = ((QueryPageViewModel)DataContext).StartDate;
            StartCombinedClock.Time = ((QueryPageViewModel)DataContext).Time;
        }

        public void StartCombinedDialogClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (Equals(eventArgs.Parameter, "1") &&
                StartCombinedCalendar.SelectedDate is DateTime selectedDate)
            {
                var combined = selectedDate.AddSeconds(StartCombinedClock.Time.TimeOfDay.TotalSeconds);
                ((QueryPageViewModel)DataContext).Time = combined;
                ((QueryPageViewModel)DataContext).StartDate = combined;
            }
        }

        public void StopCombinedDialogOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {
            StopCombinedCalendar.SelectedDate = ((QueryPageViewModel)DataContext).EndDate;
            StopCombinedClock.Time = ((QueryPageViewModel)DataContext).Time;
        }

        public void StopCombinedDialogClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (Equals(eventArgs.Parameter, "1") &&
                StopCombinedCalendar.SelectedDate is DateTime selectedDate)
            {
                var combined = selectedDate.AddSeconds(StopCombinedClock.Time.TimeOfDay.TotalSeconds);
                ((QueryPageViewModel)DataContext).Time = combined;
                ((QueryPageViewModel)DataContext).EndDate = combined;
            }
        }

        public void DefectStartCombinedDialogOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {
            DefectStartCombinedCalendar.SelectedDate = ((QueryPageViewModel)DataContext).DefectStartDate;
            DefectStartCombinedClock.Time = ((QueryPageViewModel)DataContext).DefectTime;
        }

        public void DefectStartCombinedDialogClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (Equals(eventArgs.Parameter, "1") &&
                DefectStartCombinedCalendar.SelectedDate is DateTime selectedDate)
            {
                var combined = selectedDate.AddSeconds(DefectStartCombinedClock.Time.TimeOfDay.TotalSeconds);
                ((QueryPageViewModel)DataContext).DefectTime = combined;
                ((QueryPageViewModel)DataContext).DefectStartDate = combined;
            }
        }

        public void DefectStopCombinedDialogOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {
            DefectStopCombinedCalendar.SelectedDate = ((QueryPageViewModel)DataContext).DefectEndDate;
            DefectStopCombinedClock.Time = ((QueryPageViewModel)DataContext).DefectTime;
        }

        public void DefectStopCombinedDialogClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (Equals(eventArgs.Parameter, "1") &&
                DefectStopCombinedCalendar.SelectedDate is DateTime selectedDate)
            {
                var combined = selectedDate.AddSeconds(DefectStopCombinedClock.Time.TimeOfDay.TotalSeconds);
                ((QueryPageViewModel)DataContext).DefectTime = combined;
                ((QueryPageViewModel)DataContext).DefectEndDate = combined;
            }
        }


        private void TextBoxPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            string inputText = ((TextBox)sender).Text;
            int dotlength = Regex.Matches(inputText, @"[.]").Count;
            int spacelength = Regex.Matches(inputText, " ").Count;
            if ((e.Text == "." && dotlength >= 1) || (e.Text == " " && spacelength >= 1))
            {
                e.Handled = true;
                return;
            }
            var regex = new Regex(@"[^0-9,.-]+").IsMatch(e.Text);
            e.Handled = regex;
        }
    }
}
