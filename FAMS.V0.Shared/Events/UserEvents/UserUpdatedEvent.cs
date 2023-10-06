using FAMS.V0.Shared.Entities;

namespace FAMS.V0.Shared.Events.UserEvents;

public class UserUpdatedEvent
{
    public User User { get; set; }

    public UserUpdatedEvent(User user)
    {
        User = user;
    }
}