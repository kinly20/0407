using DRsoft.Runtime.Core.Platform.Events;
using DRsoft.Runtime.Core.Platform.Logging;
using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using DRsoft.Engine.Core.Internal;
using TcpClient = DRsoft.Runtime.Core.Platform.SocketService.TcpClient;
using DRsoft.Engine.Model.Const;

namespace DRsoft.Engine.Core.Camera
{
    public abstract class MessageBase : TcpClient
    {
        #region 属性

        /// <summary>
        /// 客户端socket连接
        /// </summary>
        protected Socket ClientSocket;

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
        public int TimeOut = 2000;

        /// <summary>
        /// 事件发布
        /// </summary>
        protected PubSubEvent<EngineEventArgs> PubSubEventManager;

        /// <summary>
        /// 心跳时间
        /// </summary>
        protected int HeartTime = 1000;

        protected TaskFactory TaskFactory;
        protected CancellationTokenSource TokenSource;

        #endregion

        /// <summary>
        /// 
        /// </summary>
        public MessageBase()
        {
            Log = LogProvider.GetLogger(GetType());
            TokenSource = new CancellationTokenSource();
            TaskFactory = new TaskFactory(TokenSource.Token);
            base.Retry = 1;
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
        }

        protected override void OnDisconnected()
        {
            retryConnect++;
            // Wait for a while...
            Thread.Sleep(200);
            try
            {
                // Try to connect again
                if (!_stop && retryConnect <= base.Retry)
                    ConnectAsync();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 设置事件聚合器
        /// </summary>
        /// <param name="aggreator"></param>
        public virtual void SetEventAggregator(IEventAggregator aggreator)
        {
            PubSubEventManager = aggreator.GetEvent<PubSubEvent<EngineEventArgs>>();
        }

        /// <summary>
        /// 打开连接
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public virtual bool OpenConnection(string ip, int port)
        {
            if (string.IsNullOrEmpty(ip))
            {
                Log.Error($"OpenConnection IP Error, IP :'{ip}'");
                PubSubEventManager?.Publish(new EngineEventArgs { Handle = DRSoftEventDefine.EVENT_CAMERA_ERR });
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
            if (string.IsNullOrEmpty(Ip) || Port == 0)
            {
                Log.Error($"OpenConnection IP Error, IP :'{Ip}'");
                PubSubEventManager?.Publish(new EngineEventArgs { Handle = DRSoftEventDefine.EVENT_CAMERA_ERR });
                return;
            }

            try
            {
                IPAddress ipAddress = IPAddress.Parse(Ip);
                IPEndPoint ipe = new IPEndPoint(ipAddress, Port);

                Initialize(Ip, Port);
                ConnectAsync();
            }
            catch (Exception ex)
            {
                Log.ErrorException("ReConnection 发生异常", ex);
            }

            Log.Info($"ReConnection success, IP :'{Ip}', Port : '{Port}'");
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
                Log.Error($"OnReceive {recvMessage}");
                OnMessageRecvBack(recvMessage);
            }
            catch (Exception ex)
            {
                Log.ErrorException("返回消息处理失败", ex);
            }
        }

        public abstract void OnMessageRecvBack(string recvMessage);
    }
}