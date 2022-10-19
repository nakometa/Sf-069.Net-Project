using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SportsHub.DAL.Migrations
{
    public partial class UserArticleRelationship : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Articles_ArticleId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_ArticleId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ArticleId",
                table: "Users");

            migrationBuilder.CreateTable(
                name: "ArticleUser",
                columns: table => new
                {
                    ArticlesId = table.Column<int>(type: "int", nullable: false),
                    AuthorsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleUser", x => new { x.ArticlesId, x.AuthorsId });
                    table.ForeignKey(
                        name: "FK_ArticleUser_Articles_ArticlesId",
                        column: x => x.ArticlesId,
                        principalTable: "Articles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ArticleUser_Users_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ArticleUser_AuthorsId",
                table: "ArticleUser",
                column: "AuthorsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ArticleUser");

            migrationBuilder.AddColumn<int>(
                name: "ArticleId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_ArticleId",
                table: "Users",
                column: "ArticleId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Articles_ArticleId",
                table: "Users",
                column: "ArticleId",
                principalTable: "Articles",
                principalColumn: "Id");
        }
    }
}
