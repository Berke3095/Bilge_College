using BilgeCollege.MODELS.Concretes.CustomUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BilgeCollege.DAL.Configs
{
    public class UserRoleConfig : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.Property(x => x.CustomId).ValueGeneratedOnAdd();

            builder.HasData(GetUserRoles());
        }

        public List<UserRole> GetUserRoles()
        {
            return new List<UserRole>
            {
                new UserRole{CustomId = 1, Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN"},
                new UserRole{CustomId = 2, Id = Guid.NewGuid().ToString(), Name = "Teacher", NormalizedName = "TEACHER"},
                new UserRole{CustomId = 3, Id = Guid.NewGuid().ToString(), Name = "Guardian", NormalizedName = "GUARDIAN"}
            };
        }
    }
}
