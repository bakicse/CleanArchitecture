using Domain.Common;

namespace Domain.Master;
public class SubCategory : AuditableWithBaseEntity<int>
{
    public required string SubCategoryName { get; set; }
    public int CategoryId { get; set; }

    public virtual Category? Category { get; set; }
}
