using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SampleWebAPI.Data.Migrations
{
    public partial class tambah_field_title_tblsamurai : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Samurais",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Title",
                table: "Samurais");
        }
    }
}
