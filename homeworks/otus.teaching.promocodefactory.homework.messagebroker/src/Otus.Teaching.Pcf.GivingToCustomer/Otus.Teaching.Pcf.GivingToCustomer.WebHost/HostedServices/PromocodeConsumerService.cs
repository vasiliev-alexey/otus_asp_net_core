using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Otus.Teaching.Pcf.Administration.WebHost.Options;
using Otus.Teaching.Pcf.GivingToCustomer.Core.Abstractions.Services;
using Otus.Teaching.Pcf.GivingToCustomer.WebHost.Models;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using ExchangeType = RabbitMQ.Client.ExchangeType;


namespace Otus.Teaching.Pcf.GivingToCustomer.WebHost.HostedServices
{
    public class PromocodeConsumerService : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private const string Exchange = "Pcf.ReceivingFromPartner.Promocodes";
        private const string Queue = "Pcf.Administration.Promocodes";
        private const string RoutingKey = "Pcf.ReceivingFromPartner.Promocode";
        private readonly TaskCompletionSource _source = new();

        private readonly ConnectionFactory _connectionFactory;
        private IModel _channel;
        private IConnection _con;
        private readonly IHostApplicationLifetime _lifetime;

        public PromocodeConsumerService(IServiceScopeFactory serviceScopeFactory, IHostApplicationLifetime lifetime,
            IOptions<BusConnectOptions> busOptions)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _lifetime = lifetime;
            _lifetime.ApplicationStarted.Register(() => _source.SetResult());
            _connectionFactory = new ConnectionFactory
            {
                UserName = busOptions.Value.Username,
                Password = busOptions.Value.Password,
                HostName = busOptions.Value.Host,
                Port = busOptions.Value.Port,
                VirtualHost = busOptions.Value.VirtualHost
            };
        }

        private IModel InitConsumer()
        {
            _con = _connectionFactory.CreateConnection();
            _channel = _con.CreateModel();
            _channel.QueueDeclare(
                queue: Queue,
                exclusive: false,
                durable: true,
                autoDelete: false);


            _channel.ExchangeDeclare(
                exchange: Exchange,
                type: ExchangeType.Direct,
                durable: true);

            _channel.QueueBind(
                queue: Queue,
                exchange: Exchange,
                routingKey: RoutingKey);

            return _channel;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            if (!await WaitForAppStartup(_lifetime, stoppingToken))
            {
                return;
            }

            var initConsumer = InitConsumer();
            var consumer = new EventingBasicConsumer(initConsumer);

            consumer.Received += async (_, ea) =>
            {
                await using var scope = _serviceScopeFactory.CreateAsyncScope();
                {
                    var promocodeService = scope.ServiceProvider.GetRequiredService<IPromocodeService<GivePromoCodeRequest>>();
                    var message = Encoding.UTF8.GetString(ea.Body.ToArray());
                    var promocodeDto = JsonSerializer.Deserialize<GivePromoCodeRequest>(message);
                    await promocodeService.GivePromoCodesToCustomersWithPreferenceAsync(promocodeDto);

                    initConsumer.BasicAck(ea.DeliveryTag, false);
                }
            };
            initConsumer.BasicConsume(queue: Queue, autoAck: false, consumer: consumer);
        }

        static async Task<bool> WaitForAppStartup(IHostApplicationLifetime lifetime, CancellationToken stoppingToken)
        {
            var startedSource = new TaskCompletionSource();
            var cancelledSource = new TaskCompletionSource();

            await using var reg1 = lifetime.ApplicationStarted.Register(() => startedSource.SetResult());
            await using var reg2 = stoppingToken.Register(() => cancelledSource.SetResult());

            var completedTask = await Task.WhenAny(
                startedSource.Task,
                cancelledSource.Task).ConfigureAwait(false);

            // If the completed tasks was the "app started" task, return true, otherwise false
            return completedTask == startedSource.Task;
        }
    }
}