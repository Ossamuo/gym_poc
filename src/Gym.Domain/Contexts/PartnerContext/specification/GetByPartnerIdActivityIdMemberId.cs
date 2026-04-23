using Gym.Domain.Contexts.ActivitiesContext.Entities;
using Gym.Domain.Contexts.SharedContext.Specifications.Abstractions;

namespace Gym.Domain.Contexts.PartnerContext.specification;

public class GetByPartnerIdActivityIdMemberId(
    Guid PartnerId,
    Guid AcitivyId,
    Guid MemberId) : BaseSpecification<Booking>(x =>
    x.PartnerId == PartnerId
    &&
    x.ActivityId == AcitivyId
    &&
    x.MemberId == MemberId
);