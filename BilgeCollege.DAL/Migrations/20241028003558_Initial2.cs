using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BilgeCollege.DAL.Migrations
{
    /// <inheritdoc />
    public partial class Initial2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "855fdfb0-478d-4c5c-b2a7-b598907a4063");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "964ddff5-4e2d-4492-8722-a96a8f0f620a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9bd7c138-dddb-4695-82ec-7560dcce3983");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a2d5be5c-f49d-4519-a158-abc828b01b61");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "b8aefc7b-ce1e-4218-bff0-c7fc0e8dd92c");

            migrationBuilder.AddColumn<string>(
                name: "Day",
                table: "DaySchedules",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedDate", "Discriminator", "ModifiedDate", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2a1cd98c-3a33-481a-9cac-a16c9dcf0c8e", null, new DateTime(2024, 10, 28, 3, 35, 58, 595, DateTimeKind.Local).AddTicks(3802), "UserRole", null, "Teacher", "TEACHER" },
                    { "a12fd01d-8558-42c5-b40e-cda79414464d", null, new DateTime(2024, 10, 28, 3, 35, 58, 595, DateTimeKind.Local).AddTicks(3806), "UserRole", null, "Guardian", "GUARDIAN" },
                    { "c8461e3e-08bc-4b02-bd75-d5bf1d06d951", null, new DateTime(2024, 10, 28, 3, 35, 58, 595, DateTimeKind.Local).AddTicks(3815), "UserRole", null, "Student", "STUDENT" },
                    { "fa97653b-602e-4e3d-a82e-7f7ff2a092d4", null, new DateTime(2024, 10, 28, 3, 35, 58, 595, DateTimeKind.Local).AddTicks(3793), "UserRole", null, "Admin", "ADMIN" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "ModifiedDate", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3d0912da-37c1-4b4e-afd6-ca10268a6e81", 0, "6cb298ad-6991-4018-b6b1-e65c69e9cea2", new DateTime(2024, 10, 28, 3, 35, 58, 560, DateTimeKind.Local).AddTicks(9812), "berke_aktepe@hotmail.com", false, false, null, null, "BERKE_AKTEPE@HOTMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAENQOpykaz6gwAkasbQdUxgWzvH0qQ9dqtdWXTVbOgB9yeBoWhr+MZRkg4KHTvAMtMw==", null, false, "7e608c09-fba2-47a7-99df-f55378f9cb2d", false, "Admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2a1cd98c-3a33-481a-9cac-a16c9dcf0c8e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a12fd01d-8558-42c5-b40e-cda79414464d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c8461e3e-08bc-4b02-bd75-d5bf1d06d951");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fa97653b-602e-4e3d-a82e-7f7ff2a092d4");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3d0912da-37c1-4b4e-afd6-ca10268a6e81");

            migrationBuilder.DropColumn(
                name: "Day",
                table: "DaySchedules");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "CreatedDate", "Discriminator", "ModifiedDate", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "855fdfb0-478d-4c5c-b2a7-b598907a4063", null, new DateTime(2024, 10, 28, 3, 10, 41, 38, DateTimeKind.Local).AddTicks(323), "UserRole", null, "Admin", "ADMIN" },
                    { "964ddff5-4e2d-4492-8722-a96a8f0f620a", null, new DateTime(2024, 10, 28, 3, 10, 41, 38, DateTimeKind.Local).AddTicks(348), "UserRole", null, "Student", "STUDENT" },
                    { "9bd7c138-dddb-4695-82ec-7560dcce3983", null, new DateTime(2024, 10, 28, 3, 10, 41, 38, DateTimeKind.Local).AddTicks(344), "UserRole", null, "Guardian", "GUARDIAN" },
                    { "a2d5be5c-f49d-4519-a158-abc828b01b61", null, new DateTime(2024, 10, 28, 3, 10, 41, 38, DateTimeKind.Local).AddTicks(335), "UserRole", null, "Teacher", "TEACHER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedDate", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "ModifiedDate", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "b8aefc7b-ce1e-4218-bff0-c7fc0e8dd92c", 0, "a6f6b522-2286-4504-b92f-6935804c74eb", new DateTime(2024, 10, 28, 3, 10, 41, 3, DateTimeKind.Local).AddTicks(1687), "berke_aktepe@hotmail.com", false, false, null, null, "BERKE_AKTEPE@HOTMAIL.COM", "ADMIN", "AQAAAAIAAYagAAAAEDoel/Pgxeh1FdgRRrIdZvydJz/qwCuNAkPRBkbo5MAInwEPj156thhar7SJ8WcQlg==", null, false, "e3d3bcd9-f3fe-4f41-bbe8-912ebafde6d6", false, "Admin" });
        }
    }
}
