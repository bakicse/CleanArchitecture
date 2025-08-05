using Domain.Common;

namespace Domain.Master;
public class AppSetting : AuditableWithBaseEntity<int> {
    public string ReferenceKey { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
    public string? Description { get; set; }
    public string Type { get; set; } = string.Empty;
}
