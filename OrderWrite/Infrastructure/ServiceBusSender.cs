using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Azure.ServiceBus;
using Newtonsoft.Json;
using OrderWrite.Events;
using OrderWrite.Models;

namespace OrderWrite.Infrastructure
{
    public interface IServicebusSender {
        void SendMessage(IEvents myevent);
    }
    public class ServiceBusSender : IServicebusSender
    {
        private string _connectionString;
        private string _topicName;
        private TopicClient client;
        public ServiceBusSender(string connectionString , string topicName)
        {
            _connectionString = connectionString;
            _topicName = topicName;
            client = new TopicClient(_connectionString, _topicName);
        }
        public void SendMessage(IEvents myevent)
        {
            string mymsg = JsonConvert.SerializeObject(myevent);
            Message m = new Message();
            m.Body = System.Text.Encoding.ASCII.GetBytes(mymsg);
            client.SendAsync(m).GetAwaiter().GetResult();
        }
    }
}
