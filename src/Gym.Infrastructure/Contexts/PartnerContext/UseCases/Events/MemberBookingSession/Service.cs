using Confluent.Kafka;
using Gym.Domain.Contexts.ActivitiesContext.UseCases.Book.Events;
using Gym.Domain.Contexts.PartnerContext.UseCases.Events.MemberBookingSession.Constracts;
using Gym.Domain;

namespace Gym.Infrastructure.Contexts.PartnerContext.UseCases.Events.MemberBookingSession
{
    public class Service(IProducer<string, MemberBookingSessionEvent> producer) : IServices
    {
        private readonly IProducer<string, MemberBookingSessionEvent> _producer = producer;

        private readonly string _topic = Configuration.Kafka.Topic;       


        public async Task SendBookReservationAsync(MemberBookingSessionEvent notification, CancellationToken cancellationToken)
        {
            var kafkaMessage = new Message<string, MemberBookingSessionEvent>
            {
                Key = Guid.CreateVersion7().ToString(), // Optional: define a key
                Value = notification
            };

            try
            {

                await _producer.ProduceAsync(_topic, kafkaMessage);
                await Task.Run(() => Console.WriteLine($"Sending a notification to Partner to confirm the reservation from Services through Kafka Key = {kafkaMessage.Key}"));
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
