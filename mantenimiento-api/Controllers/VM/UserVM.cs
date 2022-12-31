using mantenimiento_api.Models;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace mantenimiento_api.Controllers.VM
{
    [DataContract]
    public class UserVM
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int IdRol { get; set; }
        [DataMember]
        public string Name { get; set; } = null!;
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Password { get; set; }
        [DataMember]
        public string Email { get; set; }
        [JsonIgnore]
        public bool Active { get; set; }

    }
}
