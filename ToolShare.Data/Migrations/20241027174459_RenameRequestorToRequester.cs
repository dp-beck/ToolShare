using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolShare.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenameRequestorToRequester : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JoinPodRequests_AspNetUsers_RequestorId",
                table: "JoinPodRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ShareRequests_AspNetUsers_ToolRequestorId",
                table: "ShareRequests");

            migrationBuilder.RenameColumn(
                name: "ToolRequestorId",
                table: "ShareRequests",
                newName: "ToolRequesterId");

            migrationBuilder.RenameIndex(
                name: "IX_ShareRequests_ToolRequestorId",
                table: "ShareRequests",
                newName: "IX_ShareRequests_ToolRequesterId");

            migrationBuilder.RenameColumn(
                name: "RequestorId",
                table: "JoinPodRequests",
                newName: "RequesterId");

            migrationBuilder.RenameIndex(
                name: "IX_JoinPodRequests_RequestorId",
                table: "JoinPodRequests",
                newName: "IX_JoinPodRequests_RequesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_JoinPodRequests_AspNetUsers_RequesterId",
                table: "JoinPodRequests",
                column: "RequesterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShareRequests_AspNetUsers_ToolRequesterId",
                table: "ShareRequests",
                column: "ToolRequesterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JoinPodRequests_AspNetUsers_RequesterId",
                table: "JoinPodRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_ShareRequests_AspNetUsers_ToolRequesterId",
                table: "ShareRequests");

            migrationBuilder.RenameColumn(
                name: "ToolRequesterId",
                table: "ShareRequests",
                newName: "ToolRequestorId");

            migrationBuilder.RenameIndex(
                name: "IX_ShareRequests_ToolRequesterId",
                table: "ShareRequests",
                newName: "IX_ShareRequests_ToolRequestorId");

            migrationBuilder.RenameColumn(
                name: "RequesterId",
                table: "JoinPodRequests",
                newName: "RequestorId");

            migrationBuilder.RenameIndex(
                name: "IX_JoinPodRequests_RequesterId",
                table: "JoinPodRequests",
                newName: "IX_JoinPodRequests_RequestorId");

            migrationBuilder.AddForeignKey(
                name: "FK_JoinPodRequests_AspNetUsers_RequestorId",
                table: "JoinPodRequests",
                column: "RequestorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ShareRequests_AspNetUsers_ToolRequestorId",
                table: "ShareRequests",
                column: "ToolRequestorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
