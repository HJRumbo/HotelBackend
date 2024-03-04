using Hotel.Core.Domain.Common;

namespace Hotel.Core.Domain.Entities
{
    public class EmergencyContact : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
    }
}
