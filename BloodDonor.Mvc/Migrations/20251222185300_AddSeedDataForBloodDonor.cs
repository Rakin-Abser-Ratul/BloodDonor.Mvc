using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BloodDonor.Mvc.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDataForBloodDonor : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "BloodDonors",
                columns: new[] { "Id", "Address", "BloodGroup", "ContactNumber", "DateOfBirth", "Email", "FullName", "LastDonationDate", "ProfilePicture", "Weight" },
                values: new object[,]
                {
                    { 1000, "123 Main St, Anytown, USA", 0, "123-456-7890", new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "john.doe@example.com", "John Doe", new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 70f },
                    { 1001, "123 Main St, Anytown, USA", 3, "987-654-3210", new DateTime(1985, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane.smith@example.com", "Jane Smith", new DateTime(2023, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, 70f }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BloodDonors",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BloodDonors",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
