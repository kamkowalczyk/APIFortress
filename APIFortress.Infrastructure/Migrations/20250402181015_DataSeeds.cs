using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace APIFortress.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DataItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataItems", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "DataItems",
                columns: new[] { "Id", "Description", "Title" },
                values: new object[,]
                {
                    { 1, "Current API security policies and guidelines.", "Security Policy" },
                    { 2, "Report detailing recent unauthorized access attempts.", "Incident Report" },
                    { 3, "Summary of recent audit logs.", "Audit Summary" },
                    { 4, "Overview of user access levels and activity.", "User Access Report" },
                    { 5, "Current system performance metrics and statistics.", "System Metrics" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DataItems");
        }
    }
}
