using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Master;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configuration.Master;
public class SubCategoryConfiguration :  IEntityTypeConfiguration<SubCategory>
{
    public void Configure(EntityTypeBuilder<SubCategory> builder)
    {
        builder
            .ToTable(nameof(SubCategory));
        builder.Property(nameof(SubCategory.SubCategoryName)).IsRequired().HasMaxLength(200);
        builder.Property(nameof(SubCategory.CategoryId)).IsRequired();
        builder.HasOne(t => t.Category).WithMany(t => t.SubCategories).HasForeignKey(t => t.CategoryId);
    }

}
