using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Application.Contexts.SharedContext.Messages
{
    public class SchemaRegistryOptions
    {
        public const string SectionName = "SchemaRegistryConfig";
        public string Url { get; set; } = string.Empty;
    }
}
