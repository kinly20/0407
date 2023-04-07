using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections;

namespace Dashboard
{
    public class DelegateCommand : ICommand
    {
        public Func<object, bool> canExecute;
        public Action<object> executeAction;
        public bool canExecuteCache;

        public DelegateCommand()
        {

        }

        public bool CanExecute(object parameter)
        {
            if (null != parameter)
            {
                Type type = parameter.GetType();
                if ("System.Windows.Controls.Slider" == type.ToString())
                {
                    System.Windows.Controls.Slider slider = (System.Windows.Controls.Slider)parameter;
                    if (slider.IsMouseCaptureWithin || slider.IsKeyboardFocusWithin)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;

        }
        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            executeAction(parameter);
        }

    }

    //public class MyCommand : ICommand
    //{
    //    Action<T> _execute;
    //    Func<T, bool> _canExecute;
    //    public event EventHandler CanExecuteChanged;
    //    public MyCommand(Action<T> execute, Func<T, bool> canExecute)
    //    {
    //        _execute = execute;
    //        _canExecute = canExecute;
    //    }

    //    public bool CanExecute(object parameter)
    //    {
    //        if (_canExecute == null)
    //            return true;
    //        else
    //            return _canExecute((T)parameter);
    //    }

    //    public void Execute(object parameter)
    //    {
    //        if (_execute != null && CanExecute(parameter))
    //        {
    //            _execute((T)parameter);
    //        }
    //    }
    //}

    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged
        {
            add
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested += value;
                }
            }
            remove
            {
                if (_canExecute != null)
                {
                    CommandManager.RequerySuggested -= value;
                }
            }
        }

        /// <summary>
        /// 判断命令是否可以执行的方法
        /// </summary>
        private Func<object, bool> _canExecute;

        /// <summary>
        /// 命令需要执行的方法
        /// </summary>
        private Action<object> _execute;

        /// <summary>
        /// 创建一个命令
        /// </summary>
        /// <param name="execute">命令要执行的方法</param>
        public RelayCommand(Action<object> execute) : this(execute, null)
        {
        }

        /// <summary>
        /// 创建一个命令
        /// </summary>
        /// <param name="execute">命令要执行的方法</param>
        /// <param name="canExecute">判断命令是否能够执行的方法</param>
        public RelayCommand(Action<object> execute, Func<object, bool> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        /// <summary>
        /// 判断命令是否可以执行
        /// </summary>
        /// <param name="parameter">命令传入的参数</param>
        /// <returns>是否可以执行</returns>
        public bool CanExecute(object parameter)
        {
            if (_canExecute == null) return true;
            return _canExecute(parameter);
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="parameter"></param>
        public void Execute(object parameter)
        {
            if (_execute != null && CanExecute(parameter))
            {
                _execute(parameter);
            }
        }
    }
}
