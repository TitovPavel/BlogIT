using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BlogIT.DB.Migrations
{
    public partial class AddAdmin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "AvatarId", "Birthday", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "Sex", "TwoFactorEnabled", "UserName" },
                values: new object[] { "2ea66a9a-1bf0-418a-a9f7-bb00b3a71955", 0, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "9a9e2880-e7d9-4b64-add4-795ac26a36bb", "Admin@admin.com", true, false, null, "ADMIN@ADMIN.COM", "ADMIN", "AQAAAAEAACcQAAAAEHuKKBDgB7OXg5nK+FMQpZGrnKhE+xrcAP3G6dMGSsh4Xt4zIfTa15arL/soZkLu2A==", null, false, "", "0", false, "Admin" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "2ea66a9a-1bf0-418a-a9f7-bb00b3a71955", "ccfabe84-124f-473b-81a0-5da1d8ab4857" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new object[] { "2ea66a9a-1bf0-418a-a9f7-bb00b3a71955", "ccfabe84-124f-473b-81a0-5da1d8ab4857" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2ea66a9a-1bf0-418a-a9f7-bb00b3a71955");
        }
    }
}
