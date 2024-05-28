using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace net_il_mio_fotoalbum.Migrations
{
    public partial class UpdatePhotoCategoryRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryPhoto_Categories_CategoriesId",
                table: "CategoryPhoto");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryPhoto_Photos_photosId",
                table: "CategoryPhoto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryPhoto",
                table: "CategoryPhoto");

            migrationBuilder.RenameTable(
                name: "CategoryPhoto",
                newName: "PhotoCategories");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryPhoto_photosId",
                table: "PhotoCategories",
                newName: "IX_PhotoCategories_photosId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PhotoCategories",
                table: "PhotoCategories",
                columns: new[] { "CategoriesId", "photosId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoCategories_Categories_CategoriesId",
                table: "PhotoCategories",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PhotoCategories_Photos_photosId",
                table: "PhotoCategories",
                column: "photosId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PhotoCategories_Categories_CategoriesId",
                table: "PhotoCategories");

            migrationBuilder.DropForeignKey(
                name: "FK_PhotoCategories_Photos_photosId",
                table: "PhotoCategories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PhotoCategories",
                table: "PhotoCategories");

            migrationBuilder.RenameTable(
                name: "PhotoCategories",
                newName: "CategoryPhoto");

            migrationBuilder.RenameIndex(
                name: "IX_PhotoCategories_photosId",
                table: "CategoryPhoto",
                newName: "IX_CategoryPhoto_photosId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryPhoto",
                table: "CategoryPhoto",
                columns: new[] { "CategoriesId", "photosId" });

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryPhoto_Categories_CategoriesId",
                table: "CategoryPhoto",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryPhoto_Photos_photosId",
                table: "CategoryPhoto",
                column: "photosId",
                principalTable: "Photos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
