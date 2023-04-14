using System.Data;
using Caliburn.Micro;
using JetBrains.Annotations;

namespace Engine.ViewModels
{
    public class ViewModelBase : Screen, IDisposable
    {
        protected IEventAggregator? Events;

        /// <summary>
        /// 是否允许清理资源,默认允许
        /// </summary>
        protected bool CanFreeResource = true;

        ~ViewModelBase()
        {
            Dispose(false);
        }

#pragma warning disable CS0169
        private FrameworkElement? _view;
#pragma warning restore CS0169
        /// <summary>
        /// 加载元素
        /// </summary>
        /// <param name="view"></param>
        protected override void OnViewLoaded(object view)
        {
            //base.OnViewLoaded(view);
            //_view = view as FrameworkElement;
            //if (_view != null) _view.Unloaded += _view_Unloaded;
        }

        /// <summary>
        /// 释放元素
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        [UsedImplicitly]
        void _view_Unloaded(object sender, RoutedEventArgs e)
        {
            //if (_view != null)
            //{
            //    FreeResource(_view);
            //    _view.Unloaded -= _view_Unloaded;
            //}

            //_view = null;
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="controlInput"></param>
        [UsedImplicitly]
        private void FreeResource(object? controlInput)
        {
            switch (controlInput)
            {
                case null:
                case DataRowView:
                case string:
                    return;
            }

            if (!CanFreeResource)
            {
                return;
            }
            var properties = controlInput.GetType().GetProperties();
            foreach (var property in properties)
            {
                switch (property.Name)
                {
                    case "Parent":
                    case "Owner":
                    case "SelectedItem":
                    case "SelectedIndex":
                    case "SelectedValue":
                        continue;
                }

                var control = property.GetValue(controlInput, null);
                switch (control)
                {
                    case Panel panel:
                    {
                        foreach (var childControl in panel.Children)
                        {
                            FreeResource(childControl);
                        }
                        panel.Children.Clear();
                        property.SetValue(controlInput, null, null);
                        continue;
                    }
                    case ContentControl contentControl when !controlInput.Equals(contentControl.Content):
                    {
                        if (contentControl.Content != null) FreeResource(contentControl.Content);
                        contentControl.Content = null;
                        property.SetValue(controlInput, null, null);
                        continue;
                    }
                    case ItemsControl itemsControl:
                    {
                        foreach (var item in itemsControl.Items)
                        {
                            FreeResource(item);
                        }
                        if (itemsControl.ItemsSource != null)
                        {
                            itemsControl.ItemsSource = null;
                        }
                        property.SetValue(controlInput, null, null);
                        break;
                    }
                }
            }

        }

        /// <summary>
        /// 虚基类释放资源
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Free managed objects.   
            }
            // Free unmanaged objects
        }

        public virtual void Initialize()
        {

        }

        public virtual void Reset()
        {
        }

        public virtual void RegisteEventAggregator(IEventAggregator? events)
        {
            this.Events = events;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}
