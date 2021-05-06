using System;
using System.Threading.Tasks;
using Snowflake.Data.Client;

namespace TestConsole
{
    class Program
    {
        static async Task Main(string[] args)

        {
            Console.WriteLine("Hello World!");
            var cs =
                "";
            var c = new SnowflakeDbConnection() { ConnectionString = cs};
            await c.OpenAsync();
            await c.CloseAsync();
        }
    }
}
