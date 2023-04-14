using DRsoft.Engine.Model.Const;
using DRsoft.Runtime.Core.Platform.Events;
using DRsoft.Runtime.Core.Platform.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TcpClient = DRsoft.Runtime.Core.Platform.SocketService.TcpClient;
using DRsoft.Engine.Core.Internal;
using DRsoft.Engine.Model.Vision;
using DRsoft.Engine.Model.Enum;

namespace DRsoft.Engine.Core.Vision.VisionCalibration
{
    public abstract class MessageBase : TcpClient
    {
        #region 属性

        /// <summary>
        /// 记录那些需要异步检测的消息
        /// </summary>
        public Dictionary<VisionCalib_Command, VisionMessageInfo> SendMassageDic;

        /// <summary>
        /// 记录那些需要同步反馈的消息
        /// </summary>
        public Dictionary<VisionCalib_Command, VisionMessageInfo> SyncMassageDic;

        /// <summary>
        /// 客户端socket连接
        /// </summary>
        protected string Ip = "";

        protected int Port = 0;
        private bool _stop;
        private int retryConnect;

        /// <summary>
        /// 日志
        /// </summary>
        protected ILog Log;

        /// <summary>
        /// 消息返回超时时间
        /// </summary>
        public int TimeOut = 200;

        /// <summary>
        /// 事件发布
        /// </summary>
        public PubSubEvent<EngineEventArgs> PubSubEventManager;

        public string SplitChar = "";

        protected TaskFactory TaskFactory;
        protected CancellationTokenSource TokenSource;
        protected IEventAggregator Aggregator;

        static ReaderWriterLockSlim _sendMassageDicLock = new ReaderWriterLockSlim();
        static ReaderWriterLockSlim _syncMassageDicLock = new ReaderWriterLockSlim();

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public MessageBase()
        {
            Log = LogProvider.GetLogger(GetType());
            TokenSource = new CancellationTokenSource();
            TaskFactory = new TaskFactory(TokenSource.Token);
            Retry = 1;
            SendMassageDic = new Dictionary<VisionCalib_Command, VisionMessageInfo>();
            SyncMassageDic = new Dictionary<VisionCalib_Command, VisionMessageInfo>();
        }

        /// <summary>
        /// 设置事件聚合器
        /// </summary>
        /// <param name="aggreator"></param>
        public virtual void SetEventAggregator(IEventAggregator aggreator)
        {
            Aggregator = aggreator;
            PubSubEventManager = aggreator.GetEvent<PubSubEvent<EngineEventArgs>>();
        }

        public void DisconnectAndStop()
        {
            _stop = true;
            DisconnectAsync();
            while (IsConnected)
            {
                Thread.Yield();
            }
        }

        protected override void OnConnected()
        {
#if DEBUG
            //Trace.WriteLine($"Chat TCP client connected a new session with Id {Id}");
#endif
        }

        protected override void OnDisconnected()
        {
            retryConnect++;
#if DEBUG
            // Trace.WriteLine($"Chat TCP client disconnected a session with Id {Id}");
#endif

            // Wait for a while...
            Thread.Sleep(200);

            try
            {
                // Try to connect again
                if (!_stop && retryConnect < Retry)
                    ConnectAsync();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 打开连接
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public virtual bool OpenConnection(string ip, int port)
        {
            if (ip == null || ip == "")
            {
                Log.Error($"OpenConnection IP Error, IP :'{ip}'");
                PubSubEventManager?.Publish(new EngineEventArgs
                    { Handle = DRSoftEventDefine.EVENT_VISIONCALIBRATION_ERR });
                return false;
            }

            try
            {
                if (IsConnected) CloseConnection();

                Initialize(ip, port);
                ConnectAsync();
            }
            catch (Exception ex)
            {
                Log.ErrorException($"OpenConnection IP Error, IP :'{ip}'", ex);
                return false;
            }

            DictionaryClear();
            Log.Info($"OpenConnection success, IP :'{ip}', Port : '{port}'");

            return true;
        }

        /// <summary>
        /// 关闭连接
        /// </summary>
        public virtual void CloseConnection()
        {
            try
            {
                if (!IsConnected)
                {
                    Log.Info($"CloseConnection fail, IsConnect :'{IsConnected}'");
                    return;
                }

                DisconnectAndStop();
                Log.Info($"CloseConnection close, IP :'{Ip}', Port : '{Port}'");

                DictionaryClear();
            }
            catch (Exception ex)
            {
                Log.ErrorException("CloseConnection 发生异常", ex);
            }
        }

        /// <summary>
        /// 重连
        /// </summary>
        public virtual void ReConnection()
        {
            if (Ip == null || Ip == "" || Port == 0)
            {
                Log.Error($"OpenConnection IP Error, IP :'{Ip}'");
                PubSubEventManager?.Publish(new EngineEventArgs
                    { Handle = DRSoftEventDefine.EVENT_VISIONCALIBRATION_ERR });
                return;
            }

            try
            {
                IPAddress ipAddress = IPAddress.Parse(Ip);
                IPEndPoint ipe = new IPEndPoint(ipAddress, Port);

                Initialize(Ip, Port);
                ConnectAsync();
                DictionaryClear();
            }
            catch (Exception ex)
            {
                Log.ErrorException("ReConnection 发生异常", ex);
            }

            Log.Info($"ReConnection success, IP :'{Ip}', Port : '{Port}'");
        }

        public virtual void DictionaryClear()
        {
            try
            {
                if (SendMassageDic != null) SendMassageDic.Clear();
                if (SyncMassageDic != null) SyncMassageDic.Clear();
            }
            finally
            {
            }
        }

        /// <summary>
        /// 发送消息并异步检测反馈信号，recvMessage为""时不检测
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="sendMassage"></param>
        /// <param name="recvMessage"></param>
        /// <returns></returns>
        public virtual void SendMessage(VisionCalib_Command commandType, string sendMassage, string recvMessage = "")
        {
            try
            {
                if (!IsConnected)
                {
                    Log.Error($"SendMessage connect not open, IP :'{Ip}', Port : '{Port}'");
                    PubSubEventManager?.Publish(new EngineEventArgs
                        { Handle = DRSoftEventDefine.EVENT_VISIONCALIBRATION_ERR });
                    return;
                }

                // 如果有返回值，则添加需要检测的反馈信号
                if (recvMessage != "")
                {
                    // 检查是否有同类消息没有返回的
                    if (SendMassageDic.ContainsKey(commandType))
                    {
                        Log.Error(
                            $"SendMessage had same message not back yet. commandType:'{commandType}', message:'{sendMassage}'");
                        PubSubEventManager?.Publish(new EngineEventArgs
                            { Handle = DRSoftEventDefine.EVENT_VISIONCALIBRATION_ERR });
                        return;
                    }

                    VisionMessageInfo messageInfo = new VisionMessageInfo();
                    messageInfo.SendMessage = sendMassage;
                    messageInfo.RecvMessage = recvMessage;
                    messageInfo.SendTime = DateTime.Now;

                    Log.Debug($"SendMessage Add sync message commandType'{commandType}'");
                    SendMassageDic.Add(commandType, messageInfo);
                    _sendMassageDicLock.ExitWriteLock();
                }

                sendMassage += SplitChar;
                byte[] sendBytes = Encoding.ASCII.GetBytes(sendMassage);
                SendAsync(sendBytes);
                Log.Info($"SendMessage message:'{sendMassage}', IP :'{Ip}', Port : '{Port}'");
            }
            catch (Exception ex)
            {
                Log.ErrorException("发送异步消息识别", ex);
            }
        }

        /// <summary>
        /// 发送消息时阻塞等待消息返回
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="sendMassage"></param>
        /// <param name="recvMessage"></param>
        /// <returns></returns>
        public virtual void SendMessageSync(VisionCalib_Command commandType, string sendMassage, out string recvMessage)
        {
            try
            {
                recvMessage = "";
                if (!IsConnected)
                {
                    Log.Error($"SendMessageSync connect not open, IP :'{Ip}', Port : '{Port}'");
                    PubSubEventManager?.Publish(new EngineEventArgs
                        { Handle = DRSoftEventDefine.EVENT_VISIONCALIBRATION_ERR });
                    return;
                }

                // 检查是否有同类消息没有返回的
                if (SyncMassageDic.ContainsKey(commandType))
                {
                    Log.Error(
                        $"SendMessageSync had same message not back yet. commandType:'{commandType}', message:'{sendMassage}'");
                    PubSubEventManager?.Publish(new EngineEventArgs
                        { Handle = DRSoftEventDefine.EVENT_VISIONCALIBRATION_ERR });
                    return;
                }

                VisionMessageInfo messageInfo = new VisionMessageInfo();
                messageInfo.SendMessage = sendMassage;
                messageInfo.RecvMessage = recvMessage;
                messageInfo.SendTime = DateTime.Now;

                Log.Debug($"SendMessageSync Add sync message commandType'{commandType}'");
                _syncMassageDicLock.EnterWriteLock();
                SyncMassageDic.Add(commandType, messageInfo);
                _syncMassageDicLock.ExitWriteLock();

                sendMassage += SplitChar;
                byte[] sendBytes = Encoding.ASCII.GetBytes(sendMassage);
                SendAsync(sendBytes);
                Log.Debug($"SendMessageSync message:'{sendMassage}', IP :'{Ip}', Port : '{Port}'");

                // 阻塞等待消息返回
                var startTime = DateTime.Now;
                while (true)
                {
                    // 超时检测
                    var curTime = DateTime.Now;
                    TimeSpan ts = curTime - startTime;
                    if (ts.TotalMilliseconds > TimeOut)
                    {
                        //if (rep)
                        //{

                        //}
                        //// 重发几次
                        //var taskRepeat = Task.Run(() =>
                        //{

                        //});
                        _syncMassageDicLock.EnterWriteLock();
                        SyncMassageDic.Remove(commandType);
                        _syncMassageDicLock.ExitWriteLock();
                        Log.Error($"SendMessageSync recv timeout, commandType:'{commandType}'");
                        PubSubEventManager?.Publish(new EngineEventArgs
                            { Handle = DRSoftEventDefine.EVENT_VISIONCALIBRATION_ERR });
                        break;
                    }

                    // 数据合法性检测
                    if (!SyncMassageDic.ContainsKey(commandType))
                    {
                        Log.Error($"SendMessageSync recv failed, message be delete. commandType:'{commandType}'");
                        PubSubEventManager?.Publish(new EngineEventArgs
                            { Handle = DRSoftEventDefine.EVENT_VISIONCALIBRATION_ERR });
                        break;
                    }

                    if (SyncMassageDic[commandType].RecvMessage != "")
                    {
                        recvMessage = SyncMassageDic[commandType].RecvMessage;
                        _syncMassageDicLock.EnterWriteLock();
                        SyncMassageDic.Remove(commandType);
                        _syncMassageDicLock.ExitWriteLock();
                        break;
                    }

                    Thread.Sleep(3);
                }
            }
            catch (Exception ex)
            {
                recvMessage = "";
                Log.ErrorException("发送消息失败", ex);
            }
        }

        /// <summary>
        /// 返回消息处理
        /// </summary>
        /// <param name="recvMessage"></param>
        protected override void OnReceived(byte[] buffer, long offset, long size)
        {
            string recvMessage = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            try
            {
                Log.Debug($"Receive message, message : '{recvMessage}'");

                // 判断是否有同步阻塞的消息
                foreach (var cmdType in SyncMassageDic.Keys)
                {
                    //if (recvMessage.Contains(cmdType.ToString()))
                    {
                        _syncMassageDicLock.EnterWriteLock();
                        SyncMassageDic[cmdType].RecvMessage = recvMessage;
                        _syncMassageDicLock.ExitWriteLock();
                        Log.Debug($"Receive syncMessage, commandType :'{cmdType}'");
                        return;
                    }
                }

                // 判断是否有需要异步检测的消息
                foreach (var cmdType in SendMassageDic.Keys)
                {
                    if (recvMessage.Contains(cmdType.ToString()))
                    {
                        // 判断是否超时
                        DateTime curTime = DateTime.Now;
                        TimeSpan ts = curTime - SendMassageDic[cmdType].SendTime;
                        if (ts.TotalMilliseconds > TimeOut)
                        {
                            _sendMassageDicLock.EnterWriteLock();
                            SendMassageDic.Remove(cmdType);
                            _sendMassageDicLock.ExitWriteLock();
                            Log.Error($"SendMessage recv timeout, commandType:'{cmdType}'");
                            PubSubEventManager?.Publish(new EngineEventArgs
                                { Handle = DRSoftEventDefine.EVENT_VISIONCALIBRATION_ERR });
                            return;
                        }

                        // 判断返回值是否正确
                        if (recvMessage.Contains(SendMassageDic[cmdType].RecvMessage))
                        {
                            _sendMassageDicLock.EnterWriteLock();
                            SendMassageDic.Remove(cmdType);
                            _sendMassageDicLock.ExitWriteLock();
                        }
                        else
                        {
                            Log.Error($"Receive is not correct, commandType :'{cmdType}'");
                            PubSubEventManager?.Publish(new EngineEventArgs
                                { Handle = DRSoftEventDefine.EVENT_VISIONCALIBRATION_ERR });
                            return;
                        }

                        _sendMassageDicLock.EnterWriteLock();
                        SendMassageDic[cmdType].RecvMessage = recvMessage;
                        _sendMassageDicLock.ExitWriteLock();
                        Log.Debug($"Receive SendMassage, commandType :'{cmdType}'");
                    }
                }

                string[] recvArr = recvMessage.Split(new string[] { "" }, StringSplitOptions.RemoveEmptyEntries);

                foreach (string recv in recvArr)
                {
                    OnMessageRecvBack(recv);
                }
            }
            catch (Exception ex)
            {
                Log?.ErrorException("返回消息处理失败", ex);
            }
        }

        protected override void OnError(SocketError error)
        {
            Log?.Error($"Chat TCP client caught an error with code {error}");
        }


        public abstract void OnMessageRecvBack(string recvMessage);
    }
}