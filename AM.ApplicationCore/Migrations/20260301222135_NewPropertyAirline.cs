using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AM.ApplicationCore.Migrations
{
    /// <inheritdoc />
    public partial class NewPropertyAirline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AirlineLogo",
                table: "Flights",
                newName: "Airline");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Airline",
                table: "Flights",
                newName: "AirlineLogo");
        }
    }
}
