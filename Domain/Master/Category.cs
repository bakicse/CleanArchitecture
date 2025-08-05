using Domain.Common;

namespace Domain.Master;
public class Category : AuditableWithBaseEntity<int>
{
    public required string CategoryName { get; set; }

    public virtual ICollection<SubCategory> SubCategories { get; set; } = new List<SubCategory>();
}
