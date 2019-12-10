using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using Rosbridge.Client;

using System.Threading;

namespace SysSolution
{
    public partial class rosinterface
    {

        public static Socket socket;

        //static List<Subscriber> subs = new List<Subscriber>();
        

        /// <summary>
        /// ROS Disconnection
        /// </summary>
        /// <returns></returns>
        public bool Disconnection()
        {
            if (Data.Instance.md != null)
            {
                Data.Instance.md.Dispose();
                Data.Instance.md = null;
                Data.Instance.isConnected = false;
            }

            return true;
        }

        #region Service Communication

        public async void Service(string topic, string msg)
        {
            if (!Data.Instance.isConnected)
            {
                return;
            }

            try
            {
                Data.Instance.srvclient = new ServiceClient(topic, Data.Instance.md);
                JArray argsList = new JArray();

                argsList = JArray.Parse(msg);

                var result = await Data.Instance.srvclient.Call(argsList.ToObject<List<dynamic>>());
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        /// <summary>
        /// Publisher 통신 Topic & Message Type 설정
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="msgType"></param>
        public async void PublisherTopicMsgtype(string topic, string msgType)
        {
            try
            {
                if (!Data.Instance.isConnected)
                {
                    return;
                }

                Data.Instance.publisher = new Publisher(topic, msgType, Data.Instance.md);
                // var task = Task.Run(() => Data.Instance.publisher.AdvertiseAsync());
                // await task;
                await Data.Instance.publisher.AdvertiseAsync();

                //Thread.Sleep(50);
            }
            catch (SocketException se)
            {
                
                Console.WriteLine("PublisherTopicMsgtype err1" + se.Message.ToString());

                //Disconnection();
            }
            catch (MessageDispatcherStartedException me)
            {
                Console.WriteLine("PublisherTopicMsgtype err2" + me.Message.ToString());
                //Disconnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine("PublisherTopicMsgtype err3" + ex.Message.ToString());
                //MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// Topic Publisher
        /// </summary>
        /// <param name="obj"></param>
        public async void publisher(JObject obj)
        {
            if (!Data.Instance.isConnected)
            {
                return;
            }

            try
            {
                //  var task = Task.Run(() => Data.Instance.publisher.PublishAsync(obj));
                //  await task;

                await Data.Instance.publisher.PublishAsync(obj);

                //Thread.Sleep(50);
            }
            catch (InvalidOperationException ie)
            {
                //Disconnection();
            }
            catch (SocketException se)
            {

                Console.WriteLine("publisher err 1" + se.Message.ToString());

                //Disconnection();
            }
            catch (Exception ex)
            {
                Console.WriteLine("publisher err2 " + ex.Message.ToString());
                //MessageBox.Show(ex.Message);
            }

        }

        /// <summary>
        /// 1개이상의 Topic 을 Subscriber 할 경우 사용
        /// </summary>
        /// <param name="topic"></param>
        /// <param name="type"></param>
        /// <param name="msgHandler"></param>
        /// <returns></returns>
        public async Task AddSubscriber(string topic, string type, MessageReceivedHandler msgHandler)
        {
            try
            {
                if (!Data.Instance.isConnected)
                {
                    return;
                }
                bool bcheck = false;
                foreach (Subscriber subitem in Data.Instance.subs)
                {
                    //같은 Topic 명이 있을 경우 subscriber에 추가 하지 않음.
                    if (subitem.Topic == topic)
                    {
                       return;
                    }
                }

                rosSubscriber sub = new rosSubscriber(topic, type, Data.Instance.md);
                Data.Instance.subs.Add(sub);

                sub.SetMessageHandler(msgHandler);
                await sub.SubscribeAsync();
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("AddSubscriber err :={0}", ex.Message.ToString());
            }
        }

        public async Task DeleteSubscriber(string topic)
        {
            try
            {
                int i = 0;
                int removeIdx = 0;
                for (i = 0; i < Data.Instance.subs.Count; i++)
                {
                    if (null != Data.Instance.subs[i])
                    {
                        rosSubscriber sub = (rosSubscriber)Data.Instance.subs[i];

                        if (Data.Instance.subs[i].Topic == topic)
                        {
                            sub.FreeMessageHandler();
                            await sub.UnsubscribeAsync();
                            //await Data.Instance.subs[i].UnsubscribeAsync();
                            removeIdx = i;
                        }
                    }
                }
                Data.Instance.subs.RemoveAt(removeIdx);
            }
            catch(Exception ex)
            {
                Console.Out.WriteLine("DeleteSubscriber Topic err :={0}", ex.Message.ToString());
            }
          /*  foreach (rosSubscriber sub in Data.Instance.subs)
            {
                if (null != sub)
                {
                    if (sub.Topic == topic)
                    {
                        sub.FreeMessageHandler();
                        await sub.UnsubscribeAsync();
                    }
                }
            }*/
        }


        public void DeleteSubscriber_Compulsion(string topic)
        {
            try
            {
                int i = 0;
                int removeIdx = 0;
                for (i = 0; i < Data.Instance.subs.Count; i++)
                {
                    if (null != Data.Instance.subs[i])
                    {
                        rosSubscriber sub = (rosSubscriber)Data.Instance.subs[i];

                        if (Data.Instance.subs[i].Topic == topic)
                        {
                            sub.FreeMessageHandler();
                            //sub.UnsubscribeAsync();
                            Data.Instance.subs[i].UnsubscribeAsync();
                            removeIdx = i;
                            break;
                        }
                    }
                }
                Data.Instance.subs.RemoveAt(removeIdx);
            }
            catch (Exception ex)
            {
                Console.Out.WriteLine("DeleteSubscriber_Compulsion err :={0}", ex.Message.ToString());
            }
            /*  foreach (rosSubscriber sub in Data.Instance.subs)
              {
                  if (null != sub)
                  {
                      if (sub.Topic == topic)
                      {
                          sub.FreeMessageHandler();
                          await sub.UnsubscribeAsync();
                      }
                  }
              }*/
        }

        public async Task DeleteAllSubscriber()
        {
            foreach (rosSubscriber sub in Data.Instance.subs)
            {
                if (null != sub)
                {
                    sub.FreeMessageHandler();
                    await sub.UnsubscribeAsync();
                }
            }
            Data.Instance.subs.Clear();
        }


        public  void DeleteAllSubscriber_Compulsion()
        {
            foreach (rosSubscriber sub in Data.Instance.subs)
            {
                if (null != sub)
                {
                    sub.FreeMessageHandler();
                    sub.UnsubscribeAsync();
                }
            }
            Data.Instance.subs.Clear();
        }
    }

    public class rosSubscriber : Subscriber
    {
        private MessageReceivedHandler _msgHandler = null;
        public rosSubscriber(string topic, string type, MessageDispatcher md) : base(topic, type, md)
        {
        }
        public void SetMessageHandler(MessageReceivedHandler msgHandler)
        {
            _msgHandler = msgHandler;
            MessageReceived += _msgHandler;
        }
        public void FreeMessageHandler()
        {
            if (null == _msgHandler)
                return;
            MessageReceived -= _msgHandler;
            _msgHandler = null;
        }
    }
}
