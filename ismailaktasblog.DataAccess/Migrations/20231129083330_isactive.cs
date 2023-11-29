using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ismailaktasblog.DataAccess.Migrations
{
    public partial class isactive : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Yorumlar",
                newName: "YorumId");

            migrationBuilder.AddColumn<bool>(
                name: "Onay",
                table: "Makale",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Onay",
                table: "Makale");

            migrationBuilder.RenameColumn(
                name: "YorumId",
                table: "Yorumlar",
                newName: "Id");
        }
    }
}
