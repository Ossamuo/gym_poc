using Gym.Domain.Contexts.ActivitiesContext.ValueObjects;
using Gym.Domain.Contexts.SharedContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Domain.Contexts.ActivitiesContext.Entities
{
    public class Booking : Entity
    {
        public Guid MemberId { get; set; }
        public Guid ActivityId { get; set; }
        public Guid PartnerId { get; set; }

        public DateTime? CheckInAt { get; set; }
        public DateTime? CancelAt { get; set; }
        public EBookingStatus Status { get; set; }

        public Booking() : base(Guid.CreateVersion7())
        {

        }

        protected Booking(Guid memberId, Guid activityId, Guid partnerId, DateTime? checkInAt, DateTime? cancelAt, EBookingStatus status):base(Guid.CreateVersion7())
        {
            MemberId = memberId;
            ActivityId = activityId;
            PartnerId = partnerId;
            CheckInAt = checkInAt;
            CancelAt = cancelAt;
            Status = status;
        }

        public static Booking Create(Guid memberId, Guid activityId, Guid partnerId, DateTime? checkInAt, DateTime? cancelAt)
        {
            //all booking muts be create as requested.
            return new Booking(memberId, activityId, partnerId, checkInAt, cancelAt, EBookingStatus.Requested);
        }
    }
}
