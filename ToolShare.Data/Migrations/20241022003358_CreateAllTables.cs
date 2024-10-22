using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolShare.Data.Migrations
{
    /// <inheritdoc />
    public partial class CreateAllTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOwnedById",
                table: "Tools");

            migrationBuilder.RenameColumn(
                name: "IsPossessedById",
                table: "Tools",
                newName: "ToolStatus");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tools",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BorrowerId",
                table: "Tools",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateDue",
                table: "Tools",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "Tools",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Pods",
                columns: table => new
                {
                    PodId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    PodManagerId = table.Column<string>(type: "TEXT", nullable: false),
                    PodManagerId1 = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pods", x => x.PodId);
                    table.ForeignKey(
                        name: "FK_Pods_AspNetUsers_PodManagerId1",
                        column: x => x.PodManagerId1,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ShareRequests",
                columns: table => new
                {
                    ShareRequestId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ToolRequestedToolId = table.Column<int>(type: "INTEGER", nullable: false),
                    ToolOwnerId = table.Column<string>(type: "TEXT", nullable: false),
                    ToolRequestorId = table.Column<string>(type: "TEXT", nullable: false),
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
                        name: "FK_ShareRequests_AspNetUsers_ToolRequestorId",
                        column: x => x.ToolRequestorId,
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

            migrationBuilder.CreateTable(
                name: "JoinPodRequests",
                columns: table => new
                {
                    JoinPodRequestId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RequestorId = table.Column<string>(type: "TEXT", nullable: false),
                    PodId = table.Column<int>(type: "INTEGER", nullable: false),
                    PodManagerId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JoinPodRequests", x => x.JoinPodRequestId);
                    table.ForeignKey(
                        name: "FK_JoinPodRequests_AspNetUsers_PodManagerId",
                        column: x => x.PodManagerId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JoinPodRequests_AspNetUsers_RequestorId",
                        column: x => x.RequestorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JoinPodRequests_Pods_PodId",
                        column: x => x.PodId,
                        principalTable: "Pods",
                        principalColumn: "PodId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tools_BorrowerId",
                table: "Tools",
                column: "BorrowerId");

            migrationBuilder.CreateIndex(
                name: "IX_Tools_OwnerId",
                table: "Tools",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_PodId",
                table: "AspNetUsers",
                column: "PodId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinPodRequests_PodId",
                table: "JoinPodRequests",
                column: "PodId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinPodRequests_PodManagerId",
                table: "JoinPodRequests",
                column: "PodManagerId");

            migrationBuilder.CreateIndex(
                name: "IX_JoinPodRequests_RequestorId",
                table: "JoinPodRequests",
                column: "RequestorId");

            migrationBuilder.CreateIndex(
                name: "IX_Pods_PodManagerId1",
                table: "Pods",
                column: "PodManagerId1");

            migrationBuilder.CreateIndex(
                name: "IX_ShareRequests_ToolOwnerId",
                table: "ShareRequests",
                column: "ToolOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareRequests_ToolRequestedToolId",
                table: "ShareRequests",
                column: "ToolRequestedToolId");

            migrationBuilder.CreateIndex(
                name: "IX_ShareRequests_ToolRequestorId",
                table: "ShareRequests",
                column: "ToolRequestorId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Pods_PodId",
                table: "AspNetUsers",
                column: "PodId",
                principalTable: "Pods",
                principalColumn: "PodId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_AspNetUsers_BorrowerId",
                table: "Tools",
                column: "BorrowerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_AspNetUsers_OwnerId",
                table: "Tools",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Pods_PodId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_Tools_AspNetUsers_BorrowerId",
                table: "Tools");

            migrationBuilder.DropForeignKey(
                name: "FK_Tools_AspNetUsers_OwnerId",
                table: "Tools");

            migrationBuilder.DropTable(
                name: "JoinPodRequests");

            migrationBuilder.DropTable(
                name: "ShareRequests");

            migrationBuilder.DropTable(
                name: "Pods");

            migrationBuilder.DropIndex(
                name: "IX_Tools_BorrowerId",
                table: "Tools");

            migrationBuilder.DropIndex(
                name: "IX_Tools_OwnerId",
                table: "Tools");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_PodId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "BorrowerId",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "DateDue",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Tools");

            migrationBuilder.RenameColumn(
                name: "ToolStatus",
                table: "Tools",
                newName: "IsPossessedById");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Tools",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "IsOwnedById",
                table: "Tools",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
