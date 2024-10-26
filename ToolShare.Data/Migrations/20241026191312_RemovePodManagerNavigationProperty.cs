using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolShare.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovePodManagerNavigationProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pods_AspNetUsers_PodManagerId1",
                table: "Pods");

            migrationBuilder.DropIndex(
                name: "IX_Pods_PodManagerId1",
                table: "Pods");

            migrationBuilder.DropColumn(
                name: "PodManagerId",
                table: "Pods");

            migrationBuilder.DropColumn(
                name: "PodManagerId1",
                table: "Pods");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PodManagerId",
                table: "Pods",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PodManagerId1",
                table: "Pods",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Pods_PodManagerId1",
                table: "Pods",
                column: "PodManagerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Pods_AspNetUsers_PodManagerId1",
                table: "Pods",
                column: "PodManagerId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
