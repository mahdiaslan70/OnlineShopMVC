using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication6.Data.Migrations
{
    /// <inheritdoc />
    public partial class SomeMinorChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImgUrl",
                table: "Products",
                newName: "ImageUrl");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ImageUrl",
                table: "Products",
                newName: "ImgUrl");
        }
    }
}
