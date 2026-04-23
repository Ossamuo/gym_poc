using Gym.Domain.Contexts.ActivitiesContext.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Domain.Contexts.ActivitiesContext.UseCases.List
{
    public record Response(
        Guid Id,
        Guid PartnerId,
        string Name,
        string Description,
        string ImageUrl,
        EBookingStatus? Status ,
        string StartAt,
        string EndAt
        );

}
