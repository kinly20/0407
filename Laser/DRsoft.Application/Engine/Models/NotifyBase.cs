using System.ComponentModel;
using System.Linq.Expressions;

namespace Engine.Models
{
    public  class NotifyBase
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyOfPropertyChange<TProperty>(Expression<Func<TProperty>> propertyName)
        {
            if (PropertyChanged != null)
            {
                var memberExpression = propertyName.Body as MemberExpression;
                if (memberExpression != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs(memberExpression.Member.Name));
                }
            }
        }

        public void NotifyOfPropertyChange(string p_propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(p_propertyName));
            }
        }

        public bool IsChanged { get; set; }

        public NotifyBase()
        {

        }

    }
}
