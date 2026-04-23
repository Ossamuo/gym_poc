using Gym.Domain.Contexts.AccountContext.ValueObjects;
using Gym.Domain.Contexts.SharedContext.Entities;

namespace Gym.Domain.Contexts.PartnerContext.Entities
{
    public class Partner : Entity
    {
        public string Name { get; set; } = string.Empty;
        public string ApiKey { get; set; } = string.Empty;
        public Password ApiSecretHash { get; set; } = new();

        protected Partner() : base(Guid.NewGuid())
        {

        }
        protected Partner(Guid id, string name, string apiKey, Password apiSecretHash) : base(id)
        {
            Name = name;
            ApiKey = apiKey;
            ApiSecretHash = apiSecretHash;
        }

        public static Partner Create(string name)
        {
            return new Partner(Guid.NewGuid(), name, Guid.NewGuid().ToString("N"), new Password());
        }
    }
}
