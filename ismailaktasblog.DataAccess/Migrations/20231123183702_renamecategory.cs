using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ismailaktasblog.DataAccess.Migrations
{
    public partial class renamecategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Kategori",
                newName: "KategoriId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "KategoriId",
                table: "Kategori",
                newName: "Id");
        }
    }
}
