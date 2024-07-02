using Microsoft.Extensions.Options;
using Otus.Teaching.Pcf.ReceivingFromPartner.WebHost.Options;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Otus.Teaching.Pcf.ReceivingFromPartner.Core.Abstractions.Services;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.WebHost.Services
{
    public class MessageService : IMessageService
    {
        private const string Exchange = "Pcf.ReceivingFromPartner.Promocodes";
        private const string RoutingKey = "Pcf.ReceivingFromPartner.Promocode";

        private readonly ConnectionFactory _connectionFactory;
        public MessageService(IOptions<BusConnectOptions> busOptions)
        {
            _connectionFactory = new()
            {
                UserName = busOptions.Value.Username,
                Password = busOptions.Value.Password,
                HostName = busOptions.Value.Host,
                Port = busOptions.Value.Port,
                VirtualHost = busOptions.Value.VirtualHost
            };

            using var con = _connectionFactory.CreateConnection();
            if (con.IsOpen)
            {
                using var channel = con.CreateModel();
     
                channel.ExchangeDeclare(
                    exchange: Exchange,
                    type: ExchangeType.Direct,
                    durable: true);
            }
        }

        public Task PublishMessage<T>(T message)
        {
            using var con = _connectionFactory.CreateConnection();
            if (con.IsOpen)
            {
                using var channel = con.CreateModel();
                var body = JsonSerializer.Serialize(message);
                var bytes = Encoding.UTF8.GetBytes(body);

                channel.BasicPublish(
                    exchange: Exchange,
                    routingKey: RoutingKey,
                    body: bytes);
            }
            return Task.CompletedTask;
        }
    }
}
