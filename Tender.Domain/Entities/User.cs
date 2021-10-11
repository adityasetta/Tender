namespace Tender.Domain.Entities
{
    using System;
    using System.Text.Json.Serialization;

    public class User
    {
        public string UserId { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        [JsonIgnore]
        public DateTime CreatedDate { get; set; }
    }
}
