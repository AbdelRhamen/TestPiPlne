
namespace Core.Models
{
    public class AuditEntity
    {
        public Guid CreatedBy { set; get; }
        public DateTime CreatedOn { set; get; }
        public Guid? UpdatedBy { set; get; }
        public DateTime? UpdatedOn { set; get; }
    }
}

