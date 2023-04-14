using System.Timers;
using DRsoft.Engine.Core.Control.AbstractController;
using DRsoft.Runtime.Core.Platform.Logging;

namespace DRsoft.Engine.Plugin.Control.Beckhoff.Implementation
{
    public partial class BeckhoffController : AbstractController
    {
        public System.Timers.Timer ActionReportFeedbackTimer = new System.Timers.Timer();

        /// <summary>
        /// 不同线程间，一次只能更新一次ActionReportFeedback
        /// </summary>
        /// <param name="actionReportFeedback"></param>
        public void WriteActionReportFeedbackSpec(ref bool actionReportFeedback)
        {
            lock (FeedbackWriteFeedback)
            {
                actionReportFeedback = false;
                //WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_ACTION_REPORT_FEEDBACK);
            }
        }

        public void WriteActionReportFeedback()
        {
            lock (FeedbackWriteFeedback)
            {
                Log.ErrorFormat("WriteActionReportFeedback, to reset action report");
                //WriteToControllerByIndex(ControllerInputIndex.IN_INDEX_ACTION_REPORT_FEEDBACK);
                NeedResetReportFeedback = true;
                NeedUpdateActionReportFeedback = false;
            }
        }

        public void ActionReportFeedbackCallback(object? sender, ElapsedEventArgs e)
        {
            string logMsg;
            ActionReportFeedbackTimer.Stop();
            if (NeedUpdateActionReportFeedback)
            {
                logMsg = $"ActionFeedbackCheck, reset ActionReportFeedback";
                Log.ErrorFormat(logMsg);
                WriteActionReportFeedback();
            }
        }
    }
}