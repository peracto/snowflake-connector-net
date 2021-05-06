using System;
using System.Diagnostics.Tracing;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Snowflake.Data.Client;

namespace TestConsole
{
    sealed class EventSourceCreatedListener: EventListener
    {
        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            base.OnEventSourceCreated(eventSource);
            Console.WriteLine($"New event source:{eventSource.Name}");
        }
    }

    sealed class EventSourceListener : EventListener
    {
        private readonly string _eventSourceName;
        private readonly StringBuilder _mb = new StringBuilder(); 

        public EventSourceListener(string name)
        {
            _eventSourceName = name;
        }
        protected override void OnEventSourceCreated(EventSource eventSource)
        {
            base.OnEventSourceCreated(eventSource);
            if(eventSource.Name == _eventSourceName)
            {
                EnableEvents(eventSource, EventLevel.LogAlways, EventKeywords.All);
            }
        }

        protected override void OnEventWritten(EventWrittenEventArgs eventData)
        {
            base.OnEventWritten(eventData);
            string message;
            lock (_mb)
            {
                _mb.Append("<- Event");
                _mb.Append(eventData.EventSource.Name);
                _mb.Append(" - ");
                _mb.Append(eventData.EventName);
                _mb.Append(":");
                _mb.AppendJoin(',', eventData.Payload);
                _mb.AppendLine("->");
                message = _mb.ToString();
                _mb.Clear();
            }
            Console.Write(message);
        }

    }
    class Program
    {



        static async Task Main(string[] args)
        {
            using var eventSourceListener = new EventSourceCreatedListener();
            using var httpClient = new HttpClient();
            using var l1 = new EventSourceListener("Microsoft-System-Net-Http");
            using var l2 = new EventSourceListener("Microsoft -System-Net-Sockets");
            using var l3 = new EventSourceListener("Microsoft-System-Net-NameResolution");
            using var l4 = new EventSourceListener("Microsoft-System-Net-HttpListener");
            using var l5 = new EventSourceListener("System.Diagnostics.Eventing.FrameworkEventSource");
            using var l6 = new EventSourceListener("Microsoft-Diagnostics-DiagnosticSource");
            using var l7 = new EventSourceListener("System.Threading.Tasks.TplEventSource");

            Console.WriteLine("Hello World!");
            var ss = EventSource.GetSources();
            foreach (var s in ss)
            {
                Console.WriteLine(s);
            }





            var cs = @"";
            var c = new SnowflakeDbConnection() { ConnectionString = cs};
            Console.WriteLine("OPeng");
            await c.OpenAsync();
            Console.WriteLine("OPen");
            var q = c.CreateCommand();
            q.CommandText = "SELECT 123;";
            Console.WriteLine("qert");
            var r = await q.ExecuteScalarAsync();
            Console.WriteLine("query {0}", r);
            await c.CloseAsync();
        }
    }
}
