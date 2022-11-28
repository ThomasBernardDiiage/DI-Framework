using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace DI_Framework
{
    public class Registration
    {
        [JsonProperty("registrationType")]
        [JsonConverter(typeof(StringEnumConverter))]
        public ServiceLifetime ServiceLifetime { get; set; }
        public string InterfaceName { get; set; } = string.Empty;
        public string AssemblyInterface { get; set; } = string.Empty;
        public string ClasseName { get; set; } = string.Empty;
        public string AssemblyClasse { get; set; } = string.Empty;
    }
}
