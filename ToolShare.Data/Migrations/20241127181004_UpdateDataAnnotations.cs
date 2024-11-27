using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ToolShare.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDataAnnotations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "DateDue",
                table: "Tools");

            migrationBuilder.AlterColumn<string>(
                name: "ToolPhotoUrl",
                table: "Tools",
                type: "TEXT",
                maxLength: 500,
                nullable: true,
                defaultValue: "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1732729454/screwdriver-1294338_1280_e5qlme.png",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Tools",
                type: "TEXT",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateBorrowed",
                table: "Tools",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pods",
                type: "TEXT",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ProfilePhotoUrl",
                table: "AspNetUsers",
                type: "TEXT",
                maxLength: 500,
                nullable: true,
                defaultValue: "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,c_fill,ar_1:1,g_auto,r_max,bo_5px_solid_red,b_rgb:262c35/v1731154587/blank-profile-picture-973460_1280_pxrhwm.png",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateBorrowed",
                table: "Tools");

            migrationBuilder.AlterColumn<string>(
                name: "ToolPhotoUrl",
                table: "Tools",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 500,
                oldNullable: true,
                oldDefaultValue: "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,ar_1:1,c_fill,g_auto,e_art:hokusai/v1732729454/screwdriver-1294338_1280_e5qlme.png");

            migrationBuilder.AlterColumn<string>(
                name: "OwnerId",
                table: "Tools",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Tools",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateOnly>(
                name: "DateDue",
                table: "Tools",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Pods",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "ProfilePhotoUrl",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldMaxLength: 500,
                oldNullable: true,
                oldDefaultValue: "https://res.cloudinary.com/dzsqoueki/image/upload/w_1000,c_fill,ar_1:1,g_auto,r_max,bo_5px_solid_red,b_rgb:262c35/v1731154587/blank-profile-picture-973460_1280_pxrhwm.png");
        }
    }
}
