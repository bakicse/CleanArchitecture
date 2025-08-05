using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Master.Dto;
public class CategoryDto
{
    public int Id { get; set; }
    public string CategoryName { get; set; } = null!;
    public bool IsDeleted { get; set; } = false;
}
