using FAMS.V0.Shared.Constants;
using FAMS.V0.Shared.Entities;
using FAMS.V0.Shared.Interfaces;

namespace FAMS.V0.Shared.Events.UserEvents;

public class UserCreatedEvent
{
    public User User { get; set; }

    public UserCreatedEvent(User user)
    {
        User = user;
    }
}