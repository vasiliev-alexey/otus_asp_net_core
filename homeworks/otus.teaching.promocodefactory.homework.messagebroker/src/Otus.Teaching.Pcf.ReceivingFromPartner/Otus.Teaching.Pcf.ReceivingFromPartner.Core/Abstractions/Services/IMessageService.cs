using System.Threading.Tasks;

namespace Otus.Teaching.Pcf.ReceivingFromPartner.Core.Abstractions.Services
{
    public interface IMessageService
    {
        Task PublishMessage<T>(T message);
    }
}
