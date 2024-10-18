﻿using BilgeCollege.MODELS.Concretes.CustomUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BilgeCollege.DAL.Configs
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.CustomId).ValueGeneratedOnAdd();

            builder.HasData(GetUsers());
        }

        public List<User> GetUsers()
        {
            PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            List<User> users = new List<User>();

            User admin = new User
            {
                CustomId = 1,
                Id = Guid.NewGuid().ToString(),
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "berke_aktepe@hotmail.com",
                NormalizedEmail = "BERKE_AKTEPE@HOTMAIL.COM",
                EmailConfirmed = true,
            };
            admin.PasswordHash = passwordHasher.HashPassword(admin, "123");
            users.Add(admin);

            return users;
        }
    }
}
