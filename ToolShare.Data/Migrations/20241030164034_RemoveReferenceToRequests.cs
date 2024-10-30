using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolShare.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveReferenceToRequests : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_JoinPodRequests_joinPodRequestsentId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "JoinPodRequests");

            migrationBuilder.DropTable(
                name: "ShareRequests");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_joinPodRequestsentId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "joinPodRequestsentId",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "joinPodRequestsentId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "JoinPodRequests",
                columns: table => new
                {
                    JoinPodRequestId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    podRequestedId = table.Column<int>(type: "INTEGER", nullable: false),
                    ReceiverId = table.Column<string>(type: "TEXT", nullable: false),
                    RequesterId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinPodRequests", x => x.JoinPodRequestId);
                    table.ForeignKey(
                        name: "FK_JoinPodRequests_AspNetUsers_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JoinPodRequests_Pods_podRequestedId",
                        column: x => x.podRequestedId,
                        principalTable: "Pods",
                        principalColumn: "PodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShareRequests",
                columns: table => new
                {
                    ShareRequestId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ToolOwnerId = table.Column<string>(type: "TEXT", nullable: false),
                    ToolRequestedToolId = table.Column<int>(type: "INTEGER", nullable: false),
                    ToolRequesterId = table.Column<string>(type: "TEXT", nullable: false),
                    DateRequested = table.Column<DateTime>(type: "TEXT", nullable: false),
                    IsShareRequested = table.Column<bool>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShareRequests", x => x.ShareRequestId);
                    table.ForeignKey(
                        name: "FK_ShareRequests_AspNetUsers_ToolOwnerId",
                        column: x => x.ToolOwnerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShareRequests_AspNetUsers_ToolRequesterId",
                        column: x => x.ToolRequesterId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ShareRequests_Tools_ToolRequestedToolId",
                        column: x => x.ToolRequestedToolId,
                        principalTable: "Tools",
                        principalColumn: "ToolId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_joinPodRequestsentId",
                table: "AspNetUsers",
                column: "joinPodRequestsentId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JoinPodRequests_podRequestedId",
                table: "JoinPodRequests",
                column: "podRequestedId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinPodRequests_ReceiverId",
                table: "JoinPodRequests",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareRequests_ToolOwnerId",
                table: "ShareRequests",
                column: "ToolOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareRequests_ToolRequestedToolId",
                table: "ShareRequests",
                column: "ToolRequestedToolId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareRequests_ToolRequesterId",
                table: "ShareRequests",
                column: "ToolRequesterId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_JoinPodRequests_joinPodRequestsentId",
                table: "AspNetUsers",
                column: "joinPodRequestsentId",
                principalTable: "JoinPodRequests",
                principalColumn: "JoinPodRequestId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
