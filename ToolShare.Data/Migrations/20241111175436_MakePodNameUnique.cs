using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolShare.Data.Migrations
{
    /// <inheritdoc />
    public partial class MakePodNameUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Pods_Name",
                table: "Pods",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pods_Name",
                table: "Pods");
        }
    }
}
