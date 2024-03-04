using Hotel.Core.Domain.Common;

namespace Hotel.Core.Domain.Entities
{
    public class Traveler : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public DateTime Birthday { get; set; }
        public int GenderId { get; set; }
        public Gender? Gender { get; set; }
        public int DocumentTypeId { get; set; }
        public DocumentType? DocumentType { get; set; }
        public string DocumentNumber { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
    }
}
