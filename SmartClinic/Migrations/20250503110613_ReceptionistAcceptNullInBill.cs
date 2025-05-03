using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class ReceptionistAcceptNullInBill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_AspNetUsers_ReceptionistId",
                table: "Bills");

            migrationBuilder.AlterColumn<string>(
                name: "ReceptionistId",
                table: "Bills",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_AspNetUsers_ReceptionistId",
                table: "Bills",
                column: "ReceptionistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bills_AspNetUsers_ReceptionistId",
                table: "Bills");

            migrationBuilder.AlterColumn<string>(
                name: "ReceptionistId",
                table: "Bills",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bills_AspNetUsers_ReceptionistId",
                table: "Bills",
                column: "ReceptionistId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
