using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YTDownloaderAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAudioModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "YtId",
                table: "Audios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "YtId",
                table: "Audios");
        }
    }
}
