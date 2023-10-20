namespace Application.Interfaces
{
    public interface IBlacklistTokenCommandHandler
    {
        void Handle(BlacklistTokenCommand command);
    }
}