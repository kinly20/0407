using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.Windows.Forms;
using System.Reflection;

namespace Publisher
{
    [ServiceBehavior]
    public class Publisher :IPublisher
    {
         public void Subscriber(Guid id)
         {
             IPublisherEvents callback = OperationContext.Current.GetCallbackChannel<IPublisherEvents>();
             //FormPublisher form = new FormPublisher();
             //FormPublisher form = Application.OpenForms[0] as FormPublisher;

             Assembly assembly = Assembly.GetExecutingAssembly();
             FormPublisher form = assembly.CreateInstance("Publisher.FormPublisher") as FormPublisher;

             form.AddSubscriber(id,callback);
            
         }
 
         public void UnSubscriber(Guid id)
         {
             IPublisherEvents callback = OperationContext.Current.GetCallbackChannel<IPublisherEvents>();
             FormPublisher form = Application.OpenForms[0] as FormPublisher;
             form.RemoveSubscriber(id,callback);
         }
    }
}
