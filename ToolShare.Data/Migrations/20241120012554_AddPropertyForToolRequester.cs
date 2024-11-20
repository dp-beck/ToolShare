using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolShare.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertyForToolRequester : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RequesterId",
                table: "Tools",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tools_RequesterId",
                table: "Tools",
                column: "RequesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_AspNetUsers_RequesterId",
                table: "Tools",
                column: "RequesterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tools_AspNetUsers_RequesterId",
                table: "Tools");

            migrationBuilder.DropIndex(
                name: "IX_Tools_RequesterId",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "RequesterId",
                table: "Tools");
        }
    }
}
