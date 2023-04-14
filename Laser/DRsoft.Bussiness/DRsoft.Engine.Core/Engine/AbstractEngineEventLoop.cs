using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DRsoft.Engine.Core.Interface;
using DRsoft.Engine.Core.Internal;
using DRsoft.Engine.Model.Const;
using DRsoft.Runtime.Core.Platform.Events;
using DRsoft.Runtime.Core.Platform.Exceptions;
using DRsoft.Runtime.Core.Platform.Logging;
using DRsoft.Engine.Model.Vision;
using DRsoft.Engine.Model.DataBaseTable;
using System.Collections.Generic;

namespace DRsoft.Engine.Core.Engine
{
    /// <summary>
    ///
    /// </summary>
    public abstract partial class AbstractEngine : IEngine
    {
        public bool IsRunning { get; set; }
        public PubSubEvent<EngineEventArgs> PubSubEventManager = null;
        /// <summary>
        /// 待接收的事件编号(句柄)
        /// </summary>
        protected static ConcurrentQueue<int> WaitResult = new ConcurrentQueue<int>();
        protected static object PAlResult = new object();
        protected event EventHandler<AggregateExceptionArg> AggregateExceptionCatched;
        protected SubscriptionToken EngineLoopToken;

        private bool TakePhotoFinished1; //龙门1拍照完成标识
        private bool TakePhotoFinished2;
        private bool PadPositionFinished1; //龙门1收到焊接位置
        private bool PadPositionFinished2;
        private bool SilicagelStatusFinished1; //龙门1收到硅胶脏污信息
        private bool SilicagelStatusFinished2;
        private bool NoAlignResult1;   //找不到偏移位置
        private bool NoAlignResult2;   //找不到偏移位置
        private float OldAlignPos1;   //上一次偏移位置
        private float OldAlignPos2;   //上一次偏移位置
        public LaserPadPosition _laserPadPosition1 = new LaserPadPosition();
        public LaserPadPosition _laserPadPosition2 = new LaserPadPosition();
        private string _silicaId = "";   //更换硅胶膜后生成新的硅胶膜ID


        /// <summary>
        /// 引擎开启线程
        /// </summary>
        public virtual void StartEngineLoop()
        {
            string msg = "{\r\n  \"groupId\": 1,\r\n  \"workStationId\": 0,\r\n  \"hash\": null,\r\n  \"command\": null,\r\n  \"status\": null,\r\n  \"status_code\": 0,\r\n  \"message\": null,\r\n  \"solder_tapes_group_list\": [{\r\n   \"wafer_id\":1,\r\n   \"status\":\"ok\",\r\n   \"status_code\":123,\r\n   \"type_name\":\"piece\",\r\n   \"type\":1,\r\n   \"message\":\"ok\",\r\n   \"solder_tapes\":[{\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,\r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n  ],\r\n   \"flow_together_tapes\":null,\r\n   \"pad_over_si_dirtys\":null,\r\n   \"snap_cost\":0.0,\r\n   \"calc_cost\":0.0\r\n  },\r\n{\r\n   \"wafer_id\":1,\r\n   \"status\":\"ok\",\r\n   \"status_code\":123,\r\n   \"type_name\":\"piece\",\r\n   \"type\":1,\r\n   \"message\":\"ok\",\r\n   \"solder_tapes\":[{\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,\r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n  ],\r\n   \"flow_together_tapes\":null,\r\n   \"pad_over_si_dirtys\":null,\r\n   \"snap_cost\":0.0,\r\n   \"calc_cost\":0.0\r\n  },\r\n{\r\n   \"wafer_id\":1,\r\n   \"status\":\"ok\",\r\n   \"status_code\":123,\r\n   \"type_name\":\"piece\",\r\n   \"type\":1,\r\n   \"message\":\"ok\",\r\n   \"solder_tapes\":[{\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,\r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n  ],\r\n   \"flow_together_tapes\":null,\r\n   \"pad_over_si_dirtys\":null,\r\n   \"snap_cost\":0.0,\r\n   \"calc_cost\":0.0\r\n  },\r\n{\r\n   \"wafer_id\":1,\r\n   \"status\":\"ok\",\r\n   \"status_code\":123,\r\n   \"type_name\":\"piece\",\r\n   \"type\":1,\r\n   \"message\":\"ok\",\r\n   \"solder_tapes\":[{\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,\r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n  ],\r\n   \"flow_together_tapes\":null,\r\n   \"pad_over_si_dirtys\":null,\r\n   \"snap_cost\":0.0,\r\n   \"calc_cost\":0.0\r\n  },\r\n{\r\n   \"wafer_id\":1,\r\n   \"status\":\"ok\",\r\n   \"status_code\":123,\r\n   \"type_name\":\"piece\",\r\n   \"type\":1,\r\n   \"message\":\"ok\",\r\n   \"solder_tapes\":[{\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,\r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n  ],\r\n   \"flow_together_tapes\":null,\r\n   \"pad_over_si_dirtys\":null,\r\n   \"snap_cost\":0.0,\r\n   \"calc_cost\":0.0\r\n  },\r\n{\r\n   \"wafer_id\":1,\r\n   \"status\":\"ok\",\r\n   \"status_code\":123,\r\n   \"type_name\":\"piece\",\r\n   \"type\":1,\r\n   \"message\":\"ok\",\r\n   \"solder_tapes\":[{\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,\r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n   {\r\n   \"x\":0.1,\r\n   \"y\":0.1,\r\n   \"a\":90,  \r\n   },\r\n  ],\r\n   \"flow_together_tapes\":null,\r\n   \"pad_over_si_dirtys\":null,\r\n   \"snap_cost\":0.0,\r\n   \"calc_cost\":0.0\r\n  }],\r\n  \"response_type\": \"take_photo\",\r\n  \"time_cost\": 0.0,\r\n  \"snap_cost\": 0.0,\r\n  \"find_pad_cost\": 0.0,\r\n  \"find_si_dirty_spots_cost\": 0.0\r\n}";
            _laserPadPosition1= LaserPadPosition.FromJson(msg);
            _laserPadPosition2= LaserPadPosition.FromJson(msg);
            // 定义状态变更事件
            if (EngineLoopToken == null)
                EngineLoopToken = PubSubEventManager.Subscribe((arg) => { SetEvent(arg.Handle); }, ThreadOption.BackgroundThread);

            // 定时器(实时读取下位机状态)
            CurrentTimerTask = BuildEngineLoopTask(CurrentTimerTask, () =>
            {
                while (!CancelToken.IsCancellationRequested)
                {
                    PubSubEventManager?.Publish(new EngineEventArgs { Handle = DRSoftEventDefine.EVENT_ENGINE_TIMER });
                    Thread.Sleep(Config.RefreshRate);
                }
            });

            // 处理业务线程
            CurrentEngineTask = BuildEngineLoopTask(CurrentEngineTask, () =>
            {
                EngineEventLoop();
            });
            // 读取控制状态
            CurrentControllerTask = BuildEngineLoopTask(CurrentControllerTask, () =>
            {
                while (!CancelToken.IsCancellationRequested)
                {
                    Controller.ReadControlLoop();
                    if (Controller.IoOutput.LedLight)
                    {
                        Controller.AlarmChange = true;
                    }
                    else
                    {
                        Controller.AlarmChange = false;
                    }
                    Thread.Sleep(100);
                }
            });
        }

        /// <summary>
        /// 构建引擎主任务
        /// </summary>
        /// <param name="task"></param>
        /// <param name="action"></param>
        protected Task BuildEngineLoopTask(Task? task, Action action)
        {
            try
            {
                if (task != null && task.Status != TaskStatus.Running)
                {
                    task.Dispose();
                    task = null;
                }
                if (task != null) return task;
            }
            catch (Exception ex)
            {
                task = null;
            }

            return EngineTaskFactory.StartNew(() =>
            {
                try
                {
                    //Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;
                    action?.Invoke();
                }
                catch (Exception ex)
                {
                    AggregateExceptionCatched?.Invoke(null, new AggregateExceptionArg()
                    {
                        AggregateException = new AggregateException(ex)
                    });
                }
            }, CancelToken.Token);
        }

        /// <summary>
        /// 构建引擎线程任务
        /// </summary>
        /// <param name="task"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        protected Task BuildEngineThreadTask(Task? task, Action action)
        {
            try
            {
                if (task != null && task.Status != TaskStatus.Running)
                {
                    task.Dispose();
                    task = null;
                }
                if (task != null) return task;
            }
            catch (Exception ex)
            {
                task = null;
            }

            return EngineThreadTaskFactory.StartNew(() =>
            {
                try
                {
                    //Thread.CurrentThread.Priority = ThreadPriority.AboveNormal;
                    action?.Invoke();
                }
                catch (Exception ex)
                {
                    AggregateExceptionCatched?.Invoke(null, new AggregateExceptionArg()
                    {
                        AggregateException = new AggregateException(ex)
                    });
                }
            }, CancelThreadToken.Token);
        }

        /// <summary>
        /// 设置事件信号
        /// </summary>
        /// <param name="handle"></param>
        public virtual void SetEvent(int handle)
        {
            lock (WaitResult)
            {
                if (!WaitResult.Any() || handle != WaitResult.Last())
                    WaitResult.Enqueue(handle);
            }
        }

        /// <summary>
        /// 执行Engine任务
        /// </summary>
        public virtual void EngineEventLoop()
        {
            while (!CancelToken.IsCancellationRequested)
            {
                Thread.Sleep(1);
                // 循环主体
                IsRunning = true;
                int waitResult = 0;
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
                        HandleEngineOnTimer();
                        break;
                    case DRSoftEventDefine.EVENT_QUIT:
                        Dispose();
                        break;
                    case DRSoftEventDefine.EVENT_VISIONPRODUCTION_SHOOTDONE1:
                        HandleEventVisionProductionShootDone1();
                        break;
                    case DRSoftEventDefine.EVENT_VISIONPRODUCTION_PADPOS1:
                        HandleEventVisionProductionPadPos1();
                        break;
                    case DRSoftEventDefine.EVENT_VISIONPRODUCTION_SILICADATA1:
                        HandleEventVisionProductionSilicaData1();
                        break;
                    case DRSoftEventDefine.EVENT_VISIONPRODUCTION_SHOOTDONE2:
                        HandleEventVisionProductionShootDone2();
                        break;
                    case DRSoftEventDefine.EVENT_VISIONPRODUCTION_PADPOS2:
                        HandleEventVisionProductionPadPos2();
                        break;
                    case DRSoftEventDefine.EVENT_VISIONPRODUCTION_SILICADATA2:
                        HandleEventVisionProductionSilicaData2();
                        break;
                }
            }
            IsRunning = false;
        }

        /// <summary>
        /// 重置Token
        /// </summary>
        public virtual void ResetToken()
        {
            CancelToken = new CancellationTokenSource();
            CancelThreadToken = new CancellationTokenSource();
        }

        /// <summary>
        /// 处理视觉生产通讯事件
        /// </summary>
        public virtual void HandleEventVisionProductionShootDone1()
        {
            try
            {
                if (VisionProduction[0].ShootDoneData != null && VisionProduction[0].ShootDoneData.ResponseType == "take_photo")
                {
                    Longmen1CameraShootTimer.Stop();        //关闭拍照完成定时器检测
                    VisionProduction[0].ShootDoneData.CleanData();
                    TakePhotoFinished1 = true;
                    Log.ErrorFormat("Receive CameraA ShootDone1 event");
                }
            }
            catch (Exception ex)
            {

                Log.DebugFormat("HandleEventVisionProductionShootDone1 Error:{0}", ex.Message);
            }
        }

        public virtual void HandleEventVisionProductionPadPos1()
        {
            try
            {
                if (VisionProduction[0].PadPosData != null && VisionProduction[0].PadPosData.ResponseType == "pad_position")
                {
                    PadPositionFinished1 = true;
                    _laserPadPosition1 = VisionProduction[0].PadPosData;
                    VisionProduction[0].PadPosData.CleanData();
                    Log.ErrorFormat("Receive CameraA PadPos1 event");
                }
            }
            catch (Exception ex)
            {

                Log.DebugFormat("HandleEventVisionProductionPadPos1 Error:{0}", ex.Message);
            }
        }

        public virtual void HandleEventVisionProductionSilicaData1()
        {
            try
            {
                if (VisionProduction[0].SilicaData != null && VisionProduction[0].SilicaData.ResponseType == "silica_gel_status")
                {
                    SilicagelStatusFinished1 = true;
                    LaserPadPosition laserPadPosition = VisionProduction[0].SilicaData;
                    Log.ErrorFormat("Receive CameraA SilicaData1 event1");
                    //如果存在脏污，将脏污数据保存到数据库
                    if (laserPadPosition.SolderTapesGroupList == null)
                    {
                        return;
                    }
                    List<DirtyTable> lstDirty = new List<DirtyTable>();
                    int count = laserPadPosition.SolderTapesGroupList.Length;
                    for (int i = 0; i < count; i++)
                    {
                        SolderTapesGroupList solderTapesObject = laserPadPosition.SolderTapesGroupList[i];
                        int dirtyCount = solderTapesObject.PadOverSiDirtys.Length;
                        for (int j = 0; j < dirtyCount; j++)
                        {
                            PadOverSiDirty padOverSiDirty = solderTapesObject.PadOverSiDirtys[j];
                            SolderTape solderTape = solderTapesObject.SolderTapes[j];
                            int dirtySpotCount = padOverSiDirty.CurrentPadDirtySpots.Length;
                            for (int z = 0; z < dirtySpotCount; z++)
                            {
                                DirtyTable dirty = new DirtyTable();
                                dirty.id = Guid.NewGuid().ToString();
                                dirty.GroupId = laserPadPosition.GroupId;
                                dirty.SilicaId = _silicaId;
                                dirty.MachineId = StationAProcess.ToString();
                                dirty.WorkStationId = laserPadPosition.WorkStationId;
                                dirty.LaserId = i;
                                dirty.IsDirty = padOverSiDirty.IsDirty.ToString();
                                dirty.DirtyX = padOverSiDirty.CurrentPadDirtySpots[z].X;
                                dirty.DirtyY = padOverSiDirty.CurrentPadDirtySpots[z].Y;
                                dirty.DirtyWidth = padOverSiDirty.CurrentPadDirtySpots[z].Width;
                                dirty.DirtyHeight = padOverSiDirty.CurrentPadDirtySpots[z].Height;
                                dirty.PadX = solderTape.X;
                                dirty.PadY = solderTape.Y;
                                dirty.Time = DateTime.Now;                                
                                lstDirty.Add(dirty);
                            }
                        }
                    }
                    if(lstDirty.Count > 0)
                    {
                        InsertListDirtyTable(lstDirty);
                    }                    
                    VisionProduction[0].SilicaData.CleanData();
                    Log.ErrorFormat("Receive CameraA SilicaData1 event2");
                }
            }
            catch (Exception ex)
            {

                Log.DebugFormat("HandleEventVisionProductionSilicaData1 Error:{0}", ex.Message);
            }
        }

        public virtual void HandleEventVisionProductionShootDone2()
        {
            try
            {
                if (VisionProduction[1].ShootDoneData != null && VisionProduction[1].ShootDoneData.ResponseType == "take_photo")
                {
                    Longmen2CameraShootTimer.Stop();        //关闭拍照完成定时器检测
                    VisionProduction[1].ShootDoneData.CleanData();
                    TakePhotoFinished2 = true;
                    Log.ErrorFormat("Receive CameraB ShootDone2 event");
                }
            }
            catch (Exception ex)
            {

                Log.DebugFormat("HandleEventVisionProductionShootDone1 Error:{0}", ex.Message);
            }
        }

        public virtual void HandleEventVisionProductionPadPos2()
        {
            try
            {
                if (VisionProduction[1].PadPosData != null && VisionProduction[1].PadPosData.ResponseType == "pad_position")
                {
                    PadPositionFinished2 = true;
                    _laserPadPosition2 = VisionProduction[1].PadPosData;
                    VisionProduction[1].PadPosData.CleanData();
                    Log.ErrorFormat("Receive CameraB PadPos2 event");
                }
            }
            catch (Exception ex)
            {

                Log.DebugFormat("HandleEventVisionProductionPadPos1 Error:{0}", ex.Message);
            }
        }

        public virtual void HandleEventVisionProductionSilicaData2()
        {
            try
            {
                if (VisionProduction[1].SilicaData != null && VisionProduction[1].SilicaData.ResponseType == "silica_gel_status")
                {
                    SilicagelStatusFinished2 = true;
                    LaserPadPosition laserPadPosition = VisionProduction[1].SilicaData;
                    Log.ErrorFormat("Receive CameraB SilicaData2 event1");
                    //如果存在脏污，将脏污数据保存到数据库
                    if (laserPadPosition.SolderTapesGroupList == null)
                    {
                        return;
                    }
                    List<DirtyTable> lstDirty = new List<DirtyTable>();
                    int count = laserPadPosition.SolderTapesGroupList.Length;
                    for (int i = 0; i < count; i++)
                    {
                        SolderTapesGroupList solderTapesObject = laserPadPosition.SolderTapesGroupList[i];
                        int dirtyCount = solderTapesObject.PadOverSiDirtys.Length;
                        for (int j = 0; j < dirtyCount; j++)
                        {
                            PadOverSiDirty padOverSiDirty = solderTapesObject.PadOverSiDirtys[j];
                            SolderTape solderTape = solderTapesObject.SolderTapes[j];
                            int dirtySpotCount = padOverSiDirty.CurrentPadDirtySpots.Length;
                            for (int z = 0; z < dirtySpotCount; z++)
                            {
                                DirtyTable dirty = new DirtyTable();
                                dirty.id = Guid.NewGuid().ToString();
                                dirty.GroupId = laserPadPosition.GroupId;
                                dirty.SilicaId = _silicaId;
                                dirty.MachineId = StationAProcess.ToString();
                                dirty.WorkStationId = laserPadPosition.WorkStationId;
                                dirty.LaserId = i;
                                dirty.IsDirty = padOverSiDirty.IsDirty.ToString();
                                dirty.DirtyX = padOverSiDirty.CurrentPadDirtySpots[z].X;
                                dirty.DirtyY = padOverSiDirty.CurrentPadDirtySpots[z].Y;
                                dirty.DirtyWidth = padOverSiDirty.CurrentPadDirtySpots[z].Width;
                                dirty.DirtyHeight = padOverSiDirty.CurrentPadDirtySpots[z].Height;
                                dirty.PadX = solderTape.X;
                                dirty.PadY = solderTape.Y;
                                dirty.Time = DateTime.Now;
                                lstDirty.Add(dirty);
                            }
                        }
                    }
                    if (lstDirty.Count > 0)
                    {
                        InsertListDirtyTable(lstDirty);
                    }
                    VisionProduction[1].SilicaData.CleanData();
                    Log.ErrorFormat("Receive CameraB SilicaData2 event2");
                }
            }
            catch (Exception ex)
            {

                Log.DebugFormat("HandleEventVisionProductionSilicaData1 Error:{0}", ex.Message);
            }
        }


        public virtual void HandleEventVisionProductionRecvMsg1()
        {
            try
            {                
                string msg = VisionProduction[0].ReceiveMsg;                
                //接收数据
                //Thread.Sleep(15);                
                // take_photo: 抓拍完成
                // pad_position: Pad 点坐标，此时 SolderTapesGroupList.SolderTapes 有值
                // silica_gel_status: 硅胶状态，此时 SolderTapesGroupList，PadOverSiDirtys 有值                
                LaserPadPosition laserPadPosition = new LaserPadPosition();
                laserPadPosition = LaserPadPosition.FromJson(msg);
                switch (laserPadPosition.ResponseType)
                {
                    case "take_photo":
                        TakePhotoFinished1 = true;
                        break;
                    case "pad_position":
                        PadPositionFinished1 = true;                        
                        _laserPadPosition1 = laserPadPosition;
                        break;
                    case "silica_gel_status":
                        SilicagelStatusFinished1 = true;
                        //如果存在脏污，将脏污数据保存到数据库
                        if (laserPadPosition.SolderTapesGroupList == null)
                        {
                            break;
                        }
                        List<DirtyTable> lstDirty = new List<DirtyTable>();
                        int count = laserPadPosition.SolderTapesGroupList.Length;
                        for (int i = 0; i < count; i++)
                        {
                            SolderTapesGroupList solderTapesObject = laserPadPosition.SolderTapesGroupList[i];
                            int dirtyCount = solderTapesObject.PadOverSiDirtys.Length;
                            for (int j = 0; j < dirtyCount; j++)
                            {
                                PadOverSiDirty padOverSiDirty = solderTapesObject.PadOverSiDirtys[j];
                                SolderTape solderTape = solderTapesObject.SolderTapes[j];
                                int dirtySpotCount = padOverSiDirty.CurrentPadDirtySpots.Length;
                                for (int z = 0; z < dirtySpotCount; z++)
                                {
                                    DirtyTable dirty = new DirtyTable();
                                    dirty.id = Guid.NewGuid().ToString();
                                    dirty.GroupId = laserPadPosition.GroupId;
                                    dirty.SilicaId = _silicaId;
                                    dirty.MachineId = StationAProcess.ToString();
                                    dirty.WorkStationId = laserPadPosition.WorkStationId;
                                    dirty.LaserId = i;
                                    dirty.IsDirty = padOverSiDirty.IsDirty.ToString();
                                    dirty.DirtyX = padOverSiDirty.CurrentPadDirtySpots[z].X;
                                    dirty.DirtyY = padOverSiDirty.CurrentPadDirtySpots[z].Y;
                                    dirty.DirtyWidth = padOverSiDirty.CurrentPadDirtySpots[z].Width;
                                    dirty.DirtyHeight = padOverSiDirty.CurrentPadDirtySpots[z].Height;
                                    dirty.PadX = solderTape.X;
                                    dirty.PadY = solderTape.Y;
                                    dirty.Time = DateTime.Now;
                                    lstDirty.Add(dirty);
                                }
                            }
                        }
                        if (lstDirty.Count > 0)
                        {
                            InsertListDirtyTable(lstDirty);
                        }
                        break;
                    default:
                        break;
                }
            }            
            catch (Exception ex)
            {
                Log.DebugFormat("HandleEventPicGrabedDone Error:{0}", ex.Message);
            }
        }
        public virtual void HandleEventVisionProductionRecvMsg2()
        {
            try
            {
                string msg = VisionProduction[1].ReceiveMsg;
                //接收数据
                //Thread.Sleep(15);                
                // take_photo: 抓拍完成
                // pad_position: Pad 点坐标，此时 SolderTapesGroupList.SolderTapes 有值
                // silica_gel_status: 硅胶状态，此时 SolderTapesGroupList，PadOverSiDirtys 有值                
                LaserPadPosition laserPadPosition = new LaserPadPosition();
                laserPadPosition = LaserPadPosition.FromJson(msg);
                switch (laserPadPosition.ResponseType)
                {
                    case "take_photo":
                        TakePhotoFinished2 = true;
                        break;
                    case "pad_position":
                        PadPositionFinished2 = true;
                        _laserPadPosition2 = laserPadPosition;
                        break;
                    case "silica_gel_status":
                        SilicagelStatusFinished2 = true;
                        //如果存在脏污，将脏污数据保存到数据库
                        if(laserPadPosition.SolderTapesGroupList==null)
                        {
                            break;
                        }
                        List<DirtyTable> lstDirty = new List<DirtyTable>();
                        int count = laserPadPosition.SolderTapesGroupList.Length;
                        for (int i = 0; i < count; i++)
                        {
                            SolderTapesGroupList solderTapesObject = laserPadPosition.SolderTapesGroupList[i];
                            int dirtyCount = solderTapesObject.PadOverSiDirtys.Length;
                            for (int j = 0; j < dirtyCount; j++)
                            {
                                PadOverSiDirty padOverSiDirty = solderTapesObject.PadOverSiDirtys[j];
                                SolderTape solderTape = solderTapesObject.SolderTapes[j];
                                int dirtySpotCount = padOverSiDirty.CurrentPadDirtySpots.Length;
                                for (int z = 0; z < dirtySpotCount; z++)
                                {
                                    DirtyTable dirty = new DirtyTable();
                                    dirty.id = Guid.NewGuid().ToString();
                                    dirty.GroupId = laserPadPosition.GroupId;
                                    dirty.SilicaId = _silicaId;
                                    dirty.MachineId = StationBProcess.ToString(); 
                                    dirty.WorkStationId = laserPadPosition.WorkStationId;
                                    dirty.LaserId = LaserNumber + i;
                                    dirty.IsDirty = padOverSiDirty.IsDirty.ToString();
                                    dirty.DirtyX = padOverSiDirty.CurrentPadDirtySpots[z].X;
                                    dirty.DirtyY = padOverSiDirty.CurrentPadDirtySpots[z].Y;
                                    dirty.DirtyWidth = padOverSiDirty.CurrentPadDirtySpots[z].Width;
                                    dirty.DirtyHeight = padOverSiDirty.CurrentPadDirtySpots[z].Height;
                                    dirty.PadX = solderTape.X;
                                    dirty.PadY = solderTape.Y;
                                    dirty.Time = DateTime.Now;
                                    lstDirty.Add(dirty);
                                }
                            }
                        }
                        if (lstDirty.Count > 0)
                        {
                            InsertListDirtyTable(lstDirty);
                        }
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
                Log.DebugFormat("HandleEventPicGrabedDone Error:{0}", ex.Message);
            }
        }
       
        /// <summary>
        /// 处理Task中的异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void AggregateExceptionProcessed(object? sender, AggregateExceptionArg e)
        {
            if (e.AggregateException != null)
                Log.Info(e.AggregateException.Message);
        }

        /// <summary>
        /// 触发异常
        /// </summary>
        /// <param name="errorInfo"></param>
        /// <param name="ex"></param>
        protected virtual void TriggerException(ErrorInfo errorInfo, Exception ex)
        {
            OnErrored?.Invoke(this, new PlatformException(errorInfo, ex));
            Log?.ErrorException(errorInfo.Message, ex);
        }

        /// <summary>
        /// 处理上料拍照完成事件
        /// </summary>
        protected virtual void HandleEventLoadPosCamera()
        {
            try
            {
            }
            catch (Exception ex)
            {
                Log.DebugFormat("HandleEventPicGrabedDone Error:{0}", ex.Message);
            }
        }
        
        /// <summary>
        /// 根据组件ID查询上一加工组件过程中是否产生了脏污,true代表硅胶膜可用，false代表硅胶膜不可用
        /// </summary>
        /// <param name="groupId"></param>
        /// <returns></returns>
        public bool SilicaIsEnable(string groupId)
        {
            bool flag = false;
            if (Simulate)
            { 
                groupId = "1"; 
            }
            try
            {
                List<DirtyTable> lstDirty = SelectDirtyTableData(groupId);
                for (int i = 0; i < lstDirty.Count; i++)
                {
                    flag = bool.Parse(lstDirty[i].IsDirty.ToString());
                    if (flag)
                    {
                        break;
                    }
                }
                return !flag;
            }
            catch (Exception ex)
            {
                Log.DebugFormat("Failed to query the dirty of the previous component:{0}", ex.Message);                
                return flag;
            }
        }
        /// <summary>
        /// 查找硅胶膜干净的地方
        /// </summary>
        /// <param name="silicaId">硅胶膜ID</param>
        /// <param name="machineId">机台ID</param>
        /// <param name="offsetPara">偏移参数</param>
        /// <param name="offsetValue">找到干净地方时偏移的值</param>
        /// <returns></returns>
        public bool FindCleanPosition(string silicaId, string machineId, float offsetPara, ref float offsetValue)
        {
            bool isFound = false;
            double offsetRange = 8; //偏移范围为正负8mm            

            if (Simulate)
            {
                silicaId = "1";
            }

            try
            {
                List<DirtyTable> lstDirty = SelectDirtyTableData(silicaId,machineId);
                int rowCount = lstDirty.Count;
                if(rowCount==0)
                {
                    return true;
                }
                //从正方向查找硅胶膜干净的位置
                offsetValue = offsetPara;
                while (offsetValue <= offsetRange)
                {
                    for (int i = 0; i < rowCount; i++)
                    {
                        double dirtyWidth = (double)lstDirty[i].DirtyWidth;
                        double distance = Math.Abs((double)lstDirty[i].PadX + offsetValue - (double)lstDirty[i].DirtyX);
                        //判断偏移后的串焊产品是否仍在脏污范围内
                        if (distance <= dirtyWidth / 2)
                        {
                            isFound = false;
                            break;
                        }
                        else
                        {
                            isFound = true;
                        }
                    }
                    if (isFound)
                    {                        
                        return isFound;
                    }
                    offsetValue += offsetPara;
                }
                //从负方向查找硅胶膜干净的位置
                offsetValue = -offsetPara;
                while (Math.Abs(offsetValue) <= offsetRange)
                {
                    for (int i = 0; i < rowCount; i++)
                    {
                        double dirtyWidth = (double)lstDirty[i].DirtyWidth;
                        double distance = Math.Abs((double)lstDirty[i].PadX + offsetValue - (double)lstDirty[i].DirtyX);
                        //判断偏移后的串焊产品是否仍在脏污范围内
                        if (distance <= dirtyWidth / 2)
                        {
                            isFound = false;
                            break;
                        }
                        else
                        {
                            isFound = true;
                        }
                    }
                    if (isFound)
                    {                        
                        return isFound;
                    }
                    offsetValue -= offsetPara;
                }
                return isFound;
            }
            catch(Exception ex)
            {
                Log.DebugFormat("Failed to query the dirty of the previous component:{0}", ex.Message);                
                return isFound;
            }
        }
        
    }
}
