using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolShare.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPhotoUrltoTool : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ToolPhotoUrl",
                table: "Tools",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToolPhotoUrl",
                table: "Tools");
        }
    }
}
