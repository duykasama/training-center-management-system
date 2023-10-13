using FAMS.V0.Shared.Domain.Entities;

namespace FAMS.V0.Shared.Events.UserEvents;

public class EventUserUpdated
{
    public User User { get; set; }

    public EventUserUpdated(User user)
    {
        User = user;
    }
}