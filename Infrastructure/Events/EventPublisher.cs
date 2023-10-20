using Application.Interfaces;

namespace Infrastructure.Events
{
    public class EventPublisher : IEventPublisher
    {
        public Task PublishAsync<TEvent>(TEvent eventToPublish) where TEvent : class
        {
            // Aquí va la lógica para publicar el evento.
            // Puede ser tan simple como registrar el evento o más complejo, 
            // como enviarlo a un sistema de colas o bus de eventos.

            // Por simplicidad, solo lo registraremos:
            Console.WriteLine($"Evento publicado: {eventToPublish.GetType().Name}");

            return Task.CompletedTask;
        }
    }

}
