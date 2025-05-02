using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SmartClinic.Migrations
{
    /// <inheritdoc />
    public partial class Register : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Patient_Image",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Receptionist_Image",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
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
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "08315c1a-38fe-4c63-b1db-24bd4e171c46");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "756d924b-cec7-4005-b796-8b91efd141cc");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a81dc085-b544-42ff-b0b1-1f6bdfe29e69");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fd1569ea-50da-4ff7-95d7-9a14d12830a4");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Patient_Image",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Receptionist_Image",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
