using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace net_il_mio_fotoalbum.Migrations
{
    public partial class RemoveDeaultValueFromAuthorId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
            name: "AuthorId",
            table: "Photos",
            type: "nvarchar(450)",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "nvarchar(450)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
            name: "AuthorId",
            table: "Photos",
            type: "nvarchar(450)",
            nullable: false,
            defaultValue: "06b43b2d-16d1-4eb8-a4ac-8838f174dccc",
            oldClrType: typeof(string),
            oldType: "nvarchar(450)");
        }
    }
}
