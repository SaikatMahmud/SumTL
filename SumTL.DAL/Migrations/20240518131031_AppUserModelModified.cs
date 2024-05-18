using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SumTL.DAL.Migrations
{
    /// <inheritdoc />
    public partial class AppUserModelModified : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "AspNetUsers",
                newName: "Address");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "AspNetUsers",
                newName: "Password");
        }
    }
}
