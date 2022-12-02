using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMT.Web.Server.Data.Migrations
{
    public partial class AddsPermissionscolumntoRolestable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RepairOrders");

            migrationBuilder.AddColumn<int>(
                name: "Permissions",
                table: "AspNetRoles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Permissions",
                table: "AspNetRoles");

            migrationBuilder.CreateTable(
                name: "RepairOrders",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SomeUniqueThingInDb = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RepairOrders", x => x.OrderId);
                });
        }
    }
}
