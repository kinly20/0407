using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Publisher
{

     [ServiceContract(CallbackContract = typeof(IPublisherEvents))]
     public interface IPublisher
     {
         [OperationContract(IsOneWay = true)]
         void Subscriber(string clientID,string clientName);                 //订阅消息

         [OperationContract(IsOneWay = true)]
         void UnSubscriber(string clientID, string clientName);              //取消订阅
     }


     public interface IPublisherEvents
     {
         [OperationContract(IsOneWay = true)]
         void PublishMessage(string message);                                //发布消息

     }
 
}
