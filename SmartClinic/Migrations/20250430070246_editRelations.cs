using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class editRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DoctorId",
                table: "Receptionists",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Receptionists_DoctorId",
                table: "Receptionists",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receptionists_Doctors_DoctorId",
                table: "Receptionists",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receptionists_Doctors_DoctorId",
                table: "Receptionists");

            migrationBuilder.DropIndex(
                name: "IX_Receptionists_DoctorId",
                table: "Receptionists");

            migrationBuilder.DropColumn(
                name: "DoctorId",
                table: "Receptionists");
        }
    }
}
