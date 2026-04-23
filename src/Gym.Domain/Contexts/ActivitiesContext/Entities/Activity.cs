using Gym.Domain.Contexts.SharedContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gym.Domain.Contexts.AccountContext.Entities;

namespace Gym.Domain.Contexts.ActivitiesContext.Entities
{
    public class Activity : Entity
    {

        #region Constructor

        protected Activity(Guid partnerId, string name, string description, string imageUrl, DateTime startAdt, DateTime endAdt) : base(Guid.NewGuid())
        {
            PartnerId = partnerId;
            Name = name;
            Description = description;
            ImageUrl = imageUrl;
            StartAdt = startAdt;
            EndAdt = endAdt;
        }

        #endregion

        #region Properties

        public Guid PartnerId { get; set; }

        public string Name { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;
                
        //public bool Booked { get; set; } = false;
        public string ImageUrl { get; set; } = string.Empty;

        public DateTime StartAdt { get; set; }
        public DateTime EndAdt { get; set; }

        public List<Member> Members { get; set; } = new();


        #endregion


        #region Factory methods

        public static Activity Create(Guid partnerId, string name, string description, string imageUrl, DateTime startAdt, DateTime endAdt)
        {
            return new Activity(partnerId, name, description, imageUrl, startAdt, endAdt);
        }

        #endregion

    }
}
