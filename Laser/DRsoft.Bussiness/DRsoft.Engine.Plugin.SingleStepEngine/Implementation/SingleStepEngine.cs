using DRsoft.Runtime.Core.Platform.Exceptions;
using DRsoft.Runtime.Core.Platform.Logging;
using System;
using System.Linq;
using System.Threading;
using DRsoft.Engine.Core.Engine;
using DRsoft.Engine.Model.Const;

namespace DRsoft.Engine.Plugin.Engine.SingleStep.Implementation
{
    /// <summary>
    /// 单步执行引擎
    /// </summary>
    public partial class SingleStepEngine : AbstractEngine
    {
        public override string Scope => "SingleStepEngine";


        public override void BuildEngine(EngineConfig config)
        {
            base.BuildEngine(config);
            if (EngineFirstStart)
            {
                //订阅从Engine下发的配方配置变更事件
                EngineFirstStart = false;
            }

            BuildPluginVisionCalibration();
        }

        /// <summary>
        /// 
        /// </summary>
        public override void EngineEventLoop()
        {
            try
            {
                while (!CancelToken.IsCancellationRequested)
                {
                    Thread.Sleep(1);
                    int waitResult;
                    IsRunning = true;
                    lock (WaitResult)
                    {
                        if (WaitResult.Any())
                            WaitResult.TryDequeue(out waitResult);
                        else
                            waitResult = -1;
                    }

                    if (waitResult < 0)
                    {
                        continue;
                    }

                    switch (waitResult)
                    {
                        case DRSoftEventDefine.EVENT_ENGINE_TIMER:
                            HandleStatusOnTimer();
                            HandleEngineOnTimer();
                            break;
                        case DRSoftEventDefine.EVENT_QUIT:
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                TriggerException(new ErrorInfo { Message = ex.Message, Scope = this.Scope }, ex);
            }

            IsRunning = false;
        }

        #region 状态刷新(预留)

        private void HandleStatusOnTimer()
        {
            try
            {
            }
            catch (Exception ex)
            {
                Log.ErrorFormat("HandleEngineOnTimer 异常：{0}", ex.Message);
            }
        }

        #endregion

        public override void HandleEngineOnTimer()
        {
        }

        #region 标定相关业务函数

        #endregion


        public override void Dispose()
        {
            base.Dispose();
            VisionCalibration?.Dispose();
        }
    }
}