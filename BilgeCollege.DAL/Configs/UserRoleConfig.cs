using BilgeCollege.MODELS.Concretes.CustomUser;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BilgeCollege.DAL.Configs
{
    public class UserRoleConfig : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasData(GetUserRoles());
        }

        public List<UserRole> GetUserRoles()
        {
            return new List<UserRole>
            {
                new UserRole{Id = Guid.NewGuid().ToString(), Name = "Admin", NormalizedName = "ADMIN"},
                new UserRole{Id = Guid.NewGuid().ToString(), Name = "Teacher", NormalizedName = "TEACHER"},
                new UserRole{Id = Guid.NewGuid().ToString(), Name = "Guardian", NormalizedName = "GUARDIAN"},
                new UserRole{Id = Guid.NewGuid().ToString(), Name = "Student", NormalizedName = "STUDENT"}
            };
        }
    }
}
