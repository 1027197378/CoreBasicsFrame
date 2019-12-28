using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetCore.BasicsFrame.Migrations
{
    public partial class addUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SYSUser",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "NvarChar(20)", nullable: true),
                    LoginName = table.Column<string>(type: "NvarChar(18)", nullable: true),
                    LoginPwd = table.Column<string>(type: "NvarChar(18)", nullable: true),
                    Age = table.Column<int>(type: "Int", nullable: true),
                    Phone = table.Column<string>(type: "NvarChar(15)", nullable: true),
                    Addrees = table.Column<string>(type: "NvarChar(100)", nullable: true),
                    LastLoginTime = table.Column<DateTime>(type: "DateTime", nullable: true),
                    CreatUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CreatTime = table.Column<DateTime>(type: "DateTime", nullable: true),
                    UpdateUser = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "DateTime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SYSUser", x => x.UserID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SYSUser");
        }
    }
}
