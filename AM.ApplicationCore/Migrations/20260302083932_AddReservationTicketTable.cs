using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AM.ApplicationCore.Migrations
{
    /// <inheritdoc />
    public partial class AddReservationTicketTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReservationTickets",
                columns: table => new
                {
                    PassengerId = table.Column<string>(type: "varchar(7)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TicketId = table.Column<int>(type: "int", nullable: false),
                    DateReservation = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservationTickets", x => new { x.PassengerId, x.TicketId, x.DateReservation });
                    table.ForeignKey(
                        name: "FK_ReservationTickets_Passengers_PassengerId",
                        column: x => x.PassengerId,
                        principalTable: "Passengers",
                        principalColumn: "PassportNumber",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReservationTickets_Tickets_TicketId",
                        column: x => x.TicketId,
                        principalTable: "Tickets",
                        principalColumn: "TicketId",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_ReservationTickets_TicketId",
                table: "ReservationTickets",
                column: "TicketId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReservationTickets");
        }
    }
}
