using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SchoolAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Schools",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Sections = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Director = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<double>(type: "float", nullable: false),
                    WebSite = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Schools", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Schools",
                columns: new[] { "Id", "Director", "Name", "Rating", "Sections", "WebSite" },
                values: new object[,]
                {
                    { 1, "Najwa Sokkri", "ENISo", 3.5, "IA, GTE, GMP, MECA, EI", "https://eniso.rnu.tn/" },
                    { 2, "Mohsen Abid", "ENIM", 3.2999999999999998, "Textile, Mécanique, Electronique, Energitique", "https://enim.rnu.tn/" },
                    { 3, "Yosri Farhat", "ENICAR", 3.3999999999999999, "Infotronique, Informatique, Mecatronique", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Schools");
        }
    }
}
