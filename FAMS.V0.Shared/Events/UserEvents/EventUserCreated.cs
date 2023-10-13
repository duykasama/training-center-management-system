using FAMS.V0.Shared.Constants;
using FAMS.V0.Shared.Domain.Entities;
using FAMS.V0.Shared.Interfaces;

namespace FAMS.V0.Shared.Events.UserEvents;

public class EventUserCreated
{
    public User User { get; set; }

    public EventUserCreated(User user)
    {
        User = user;
    }
}