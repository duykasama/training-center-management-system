using FAMS.V0.Shared.Domain.Entities;
using FAMS.V0.Shared.Events.UserEvents;
using FAMS.V0.Shared.Interfaces;
using MassTransit;

namespace FAMS.V0.Services.UserService.Consumers;

public class ConsumerUserCreated : IConsumer<EventUserCreated>
{
    private readonly IRepository<User> _userRepository;

    public ConsumerUserCreated(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }
    
    public async Task Consume(ConsumeContext<EventUserCreated> context)
    {

        await _userRepository.DeleteAsync(context.Message.User.Id);
        Console.WriteLine("User deleted");
    }
}