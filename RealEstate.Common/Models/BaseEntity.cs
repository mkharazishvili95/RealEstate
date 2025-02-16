namespace RealEstate.Common.Models
{
    public class BaseEntity { }
    public abstract class AuditableEntity
    {
        public bool IsDeleted { get; set; }

    }
}
