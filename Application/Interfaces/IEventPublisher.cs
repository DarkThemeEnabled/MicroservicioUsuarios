namespace Application.Interfaces
{
    public interface IEventPublisher
    {
        Task PublishAsync<TEvent>(TEvent eventToPublish) where TEvent : class;
    }
}
