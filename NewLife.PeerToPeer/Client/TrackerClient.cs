﻿using System.Net;
using NewLife.Collections;
using NewLife.Net.Application;

namespace NewLife.PeerToPeer.Client
{
    /// <summary>
    /// 跟踪客户端
    /// </summary>
    /// <remarks>
    /// 每一个P2P客户端，对于每一个Tracker服务器，只有唯一的一个TrackerClient
    /// </remarks>
    public class TrackerClient : UdpStreamServer
    {
        #region 属性
        //private IPEndPoint _Address;
        ///// <summary>地址</summary>
        //public IPEndPoint Address
        //{
        //    get { return _Address; }
        //    private set { _Address = value; }
        //}
        #endregion

        #region 构造
        private TrackerClient(IPEndPoint ep) { Address = ep.Address; Port = ep.Port; }

        static DictionaryCache<IPEndPoint, TrackerClient> cache = new DictionaryCache<IPEndPoint, TrackerClient>();
        /// <summary>
        /// 根据指定地址创建一个跟踪客户端实例
        /// </summary>
        /// <param name="ep"></param>
        /// <returns></returns>
        public static TrackerClient Create(IPEndPoint ep)
        {
            return cache.GetItem(ep, delegate(IPEndPoint key)
            {
                return new TrackerClient(key);
            });
        }
        #endregion

        #region 开始/停止
        /// <summary>
        /// 已重载。
        /// </summary>
        protected override void EnsureCreateServer()
        {
            Name = "Tracker客户端 " + Address;

            // 允许同时处理多个数据包
            Server.NoDelay = true;
            // 使用线程池来处理事件
            Server.UseThreadPool = true;
        }
        #endregion

        #region 发送
        //public void Send(Byte[] buffer)
        //{
        //    EnsureCreateServer();

        //    udps
        //}
        #endregion
    }
}