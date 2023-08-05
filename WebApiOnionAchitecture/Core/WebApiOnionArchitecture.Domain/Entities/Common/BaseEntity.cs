namespace WebApiOnionArchitecture.Domain.Entities.Common
{
    public class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public virtual bool IsDeleted { get; set; } // Virtual because not every class that inherits from BaseEntity require it.
    }
}
