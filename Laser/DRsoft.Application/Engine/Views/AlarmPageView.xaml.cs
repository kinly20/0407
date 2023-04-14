using Engine.ViewModels;
using MaterialDesignThemes.Wpf;

namespace Engine.Views
{
    /// <summary>
    /// Interaction logic for AlarmPageView.xaml
    /// </summary>
    public partial class AlarmPageView : UserControl
    {
        public AlarmPageView()
        {
            InitializeComponent();
        }

        public void StartCombinedDialogOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {
            StartCombinedCalendar.SelectedDate = ((AlarmPageViewModel)DataContext).StartDate;
            StartCombinedClock.Time = ((AlarmPageViewModel)DataContext).Time;
        }

        public void StartCombinedDialogClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (Equals(eventArgs.Parameter, "1") &&
                StartCombinedCalendar.SelectedDate is DateTime selectedDate)
            {
                var combined = selectedDate.AddSeconds(StartCombinedClock.Time.TimeOfDay.TotalSeconds);
                ((AlarmPageViewModel)DataContext).Time = combined;
                ((AlarmPageViewModel)DataContext).StartDate = combined;
            }
        }

        public void StopCombinedDialogOpenedEventHandler(object sender, DialogOpenedEventArgs eventArgs)
        {
            StopCombinedCalendar.SelectedDate = ((AlarmPageViewModel)DataContext).EndDate;
            StopCombinedClock.Time = ((AlarmPageViewModel)DataContext).Time;
        }

        public void StopCombinedDialogClosingEventHandler(object sender, DialogClosingEventArgs eventArgs)
        {
            if (Equals(eventArgs.Parameter, "1") &&
                StopCombinedCalendar.SelectedDate is DateTime selectedDate)
            {
                var combined = selectedDate.AddSeconds(StopCombinedClock.Time.TimeOfDay.TotalSeconds);
                ((AlarmPageViewModel)DataContext).Time = combined;
                ((AlarmPageViewModel)DataContext).EndDate = combined;
            }
        }
    }
}
