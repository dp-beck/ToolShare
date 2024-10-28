using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolShare.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateForeignKeyRelationsForJoinPodRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Pods_PodId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_JoinPodRequests_AspNetUsers_PodManagerId",
                table: "JoinPodRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_JoinPodRequests_AspNetUsers_RequesterId",
                table: "JoinPodRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_JoinPodRequests_Pods_PodId",
                table: "JoinPodRequests");

            migrationBuilder.DropIndex(
                name: "IX_JoinPodRequests_RequesterId",
                table: "JoinPodRequests");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PodId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "PodManagerId",
                table: "JoinPodRequests",
                newName: "ReceiverId");

            migrationBuilder.RenameColumn(
                name: "PodId",
                table: "JoinPodRequests",
                newName: "podRequestedId");

            migrationBuilder.RenameIndex(
                name: "IX_JoinPodRequests_PodManagerId",
                table: "JoinPodRequests",
                newName: "IX_JoinPodRequests_ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_JoinPodRequests_PodId",
                table: "JoinPodRequests",
                newName: "IX_JoinPodRequests_podRequestedId");

            migrationBuilder.RenameColumn(
                name: "PodId",
                table: "AspNetUsers",
                newName: "joinPodRequestsentId");

            migrationBuilder.AddColumn<int>(
                name: "PodJoinedId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PodManagedId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_joinPodRequestsentId",
                table: "AspNetUsers",
                column: "joinPodRequestsentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PodJoinedId",
                table: "AspNetUsers",
                column: "PodJoinedId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PodManagedId",
                table: "AspNetUsers",
                column: "PodManagedId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_JoinPodRequests_joinPodRequestsentId",
                table: "AspNetUsers",
                column: "joinPodRequestsentId",
                principalTable: "JoinPodRequests",
                principalColumn: "JoinPodRequestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Pods_PodJoinedId",
                table: "AspNetUsers",
                column: "PodJoinedId",
                principalTable: "Pods",
                principalColumn: "PodId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Pods_PodManagedId",
                table: "AspNetUsers",
                column: "PodManagedId",
                principalTable: "Pods",
                principalColumn: "PodId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JoinPodRequests_AspNetUsers_ReceiverId",
                table: "JoinPodRequests",
                column: "ReceiverId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JoinPodRequests_Pods_podRequestedId",
                table: "JoinPodRequests",
                column: "podRequestedId",
                principalTable: "Pods",
                principalColumn: "PodId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_JoinPodRequests_joinPodRequestsentId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Pods_PodJoinedId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Pods_PodManagedId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_JoinPodRequests_AspNetUsers_ReceiverId",
                table: "JoinPodRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_JoinPodRequests_Pods_podRequestedId",
                table: "JoinPodRequests");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_joinPodRequestsentId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PodJoinedId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PodManagedId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PodJoinedId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PodManagedId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "podRequestedId",
                table: "JoinPodRequests",
                newName: "PodId");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "JoinPodRequests",
                newName: "PodManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_JoinPodRequests_ReceiverId",
                table: "JoinPodRequests",
                newName: "IX_JoinPodRequests_PodManagerId");

            migrationBuilder.RenameIndex(
                name: "IX_JoinPodRequests_podRequestedId",
                table: "JoinPodRequests",
                newName: "IX_JoinPodRequests_PodId");

            migrationBuilder.RenameColumn(
                name: "joinPodRequestsentId",
                table: "AspNetUsers",
                newName: "PodId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinPodRequests_RequesterId",
                table: "JoinPodRequests",
                column: "RequesterId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PodId",
                table: "AspNetUsers",
                column: "PodId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Pods_PodId",
                table: "AspNetUsers",
                column: "PodId",
                principalTable: "Pods",
                principalColumn: "PodId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JoinPodRequests_AspNetUsers_PodManagerId",
                table: "JoinPodRequests",
                column: "PodManagerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JoinPodRequests_AspNetUsers_RequesterId",
                table: "JoinPodRequests",
                column: "RequesterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JoinPodRequests_Pods_PodId",
                table: "JoinPodRequests",
                column: "PodId",
                principalTable: "Pods",
                principalColumn: "PodId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
