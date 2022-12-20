using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YTDownloaderAPI.Migrations
{
    /// <inheritdoc />
    public partial class Migrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Audios_PlayLists_PlayListId",
                table: "Audios");

            migrationBuilder.AlterColumn<int>(
                name: "PlayListId",
                table: "Audios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Audios_PlayLists_PlayListId",
                table: "Audios",
                column: "PlayListId",
                principalTable: "PlayLists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Audios_PlayLists_PlayListId",
                table: "Audios");

            migrationBuilder.AlterColumn<int>(
                name: "PlayListId",
                table: "Audios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Audios_PlayLists_PlayListId",
                table: "Audios",
                column: "PlayListId",
                principalTable: "PlayLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
