using DRsoft.Runtime.Core.Platform.Config;

namespace DRsoft.Engine.Model.Controller
{
    public class SendMessageToView : ConfigEventBase, IConfigExt<SendMessageToView>
    {
        public string? Message { get; set; }
        public bool Changed(SendMessageToView obj)
        {
            if (obj == null)
            {
                return false;
            }
            return obj.Message != Message;
        }

        public SendMessageToView Clone()
        {
            return new SendMessageToView
            {
                Message = this.Message
            };
        }
    }
}
