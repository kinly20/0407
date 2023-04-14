using DRsoft.PTPEngine.Model.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRsoft.PTPEngine.Core.Mediator
{
    /// <summary>
    /// 
    /// </summary>
    public class SDBMediator : IDisposable
    {
        private int PortNum;
        private int OpenRetries;
        private string[] CommBuffer;
        private bool ErrorState;
        private string ErrorMessage;
        private int LeftDistanceOffset;
        private int RightDistanceOffset;
        private int LeftBoosterOffset;
        private int RightBoosterOffset;
        private int BoosterSignalStart;
        private ExpectedResponse ExpectedResponse;
        private int PDFrequency;
        private double SavedRightDelay;
        private bool initialized;
        private bool ActivationReady;
        private MediatorConfig config;
        static SDBMediator Channel;

        public SDBMediator(MediatorConfig config)
        {
            Channel = this;
            config = config;
            CommBuffer = new string[9];
        }

        public void Initialize()
        { }

        public void CloseComm()
        {
        }

        public void OpenComm()
        {
        }

        public void ReadyForActivation()
        {
        }

        public void CalcOffsetsDelays()
        {
        }

        public void CommResponse(int code, int buf_size, string buffer)
        {
        }

        public void CheckRangeResponse(int buf_size, string buffer)
        {
        }

        public void SetBoosterDelays(double LeftDelay_us, double RightDelay_us)
        {
        }

        public void SetLeftBoostRange(int start, int length)
        {
        }

        public void SetRightBoostRange(int start, int length)
        {
        }

        public void SendBoostRange(string Address, int start, int length)
        {
        }

        public string GetErrorState()
        {
            return "";
        }

        public void GetPDFrequency()
        {
        }

        public void GetFPGAVersion()
        {
        }

        public void GetFirmwareVersion()
        {
        }

        public void ClearErrors()
        { 
        }

        public void Dispose()
        {

        }
    }
}
