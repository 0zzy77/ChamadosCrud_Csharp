using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChamadosCRUD.Migrations
{
    /// <inheritdoc />
    public partial class AlterTableTicketsTableStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_StatusTickets_StatusTicketId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "StatusTicketId",
                table: "Tickets",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_StatusTickets_StatusTicketId",
                table: "Tickets",
                column: "StatusTicketId",
                principalTable: "StatusTickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tickets_StatusTickets_StatusTicketId",
                table: "Tickets");

            migrationBuilder.AlterColumn<int>(
                name: "StatusTicketId",
                table: "Tickets",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Tickets_StatusTickets_StatusTicketId",
                table: "Tickets",
                column: "StatusTicketId",
                principalTable: "StatusTickets",
                principalColumn: "Id");
        }
    }
}
