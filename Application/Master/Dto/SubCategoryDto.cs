using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Master.Dto;
public class SubCategoryDto
{
    public int Id { get; set; }
    public string SubCategoryName { get; set; } = null!;
    public int CategoryId { get; set; }
    public bool IsDeleted { get; set; } = false;
}
