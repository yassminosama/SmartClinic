using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDecimalPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "1aa1d242-faef-4d4e-b555-8a6360694258");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "255d13ff-0b5a-4705-9af3-342189cf4a8a");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "35dbbcfb-ceb0-4f8f-b9c2-35475caa6b2d");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "d34f019b-e8da-4584-a238-b6167a631b8b");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "08315c1a-38fe-4c63-b1db-24bd4e171c46", "cb0bf9cc-76b2-4a01-8e66-378b16165d88", "Receptionist", "RECEPTIONIST" },
                    { "756d924b-cec7-4005-b796-8b91efd141cc", "3a68da19-f24e-4967-afaa-fe06f99e8fe9", "Admin", "ADMIN" },
                    { "a81dc085-b544-42ff-b0b1-1f6bdfe29e69", "296e52b6-a3d3-446e-81a9-14bab21686ca", "Doctor", "DOCTOR" },
                    { "fd1569ea-50da-4ff7-95d7-9a14d12830a4", "dea9bd91-b91b-4486-858f-ca80ae5305d3", "Patient", "PATIENT" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "08315c1a-38fe-4c63-b1db-24bd4e171c46");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "756d924b-cec7-4005-b796-8b91efd141cc");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "a81dc085-b544-42ff-b0b1-1f6bdfe29e69");

            migrationBuilder.DeleteData(
                table: "IdentityRole",
                keyColumn: "Id",
                keyValue: "fd1569ea-50da-4ff7-95d7-9a14d12830a4");

            migrationBuilder.InsertData(
                table: "IdentityRole",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1aa1d242-faef-4d4e-b555-8a6360694258", "779e864f-dd6c-470c-823e-e0fa3a2189f6", "Receptionist", "RECEPTIONIST" },
                    { "255d13ff-0b5a-4705-9af3-342189cf4a8a", "1f0f8a73-9545-4900-ac74-fa96f61f7488", "Patient", "PATIENT" },
                    { "35dbbcfb-ceb0-4f8f-b9c2-35475caa6b2d", "df0cfd69-1e14-45af-aba8-5009a81ace5c", "Doctor", "DOCTOR" },
                    { "d34f019b-e8da-4584-a238-b6167a631b8b", "3b53cd70-504a-4558-bdec-200c79519ae0", "Admin", "ADMIN" }
                });
        }
    }
}
