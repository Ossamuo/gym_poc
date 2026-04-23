using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Domain.Contexts.ActivitiesContext.ValueObjects
{
    public enum EBookingStatus
    {
        Requested = 1,   
        Sent = 2,        
        Approved = 3,    
        Rejected = 4     
    }
}
