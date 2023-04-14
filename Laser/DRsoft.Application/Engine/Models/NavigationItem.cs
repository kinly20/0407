using MaterialDesignThemes.Wpf;

namespace Engine.Models
{
    public class NavigationItem : ModelBase
    {
        public string? Title { get; set; }
        public PackIconKind SelectedIcon { get; set; }
        public PackIconKind UnselectedIcon { get; set; }
        private object? _notification = null;

        public object? Notification
        {
            get => _notification;
            set => SetProperty(ref _notification, value);
        }
    }
}
