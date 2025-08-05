using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Master;
public class ReferenceField : AuditableWithBaseEntity<long>
{
    public string Title { get; set; } = null!;

    public string ReferenceType { get; set; } = null!;

}
