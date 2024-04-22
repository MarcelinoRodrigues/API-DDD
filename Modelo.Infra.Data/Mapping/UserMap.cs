using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Modelo.Domain.Entities;

namespace Modelo.Infra.Data.Mapping
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(prop => prop.Id);

            builder.Property(prop => prop.Name)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired();

            builder.Property(prop => prop.Email)
               .HasConversion(prop => prop.ToString(), prop => prop)
               .IsRequired();

            builder.Property(prop => prop.Password)
                .HasConversion(prop => prop.ToString(), prop => prop)
                .IsRequired();
        }
    }
}
