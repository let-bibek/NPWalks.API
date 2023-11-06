using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace NPWalks.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataforDifficultiesandRegionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Difficulties",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("335367c5-70b1-4d7f-9163-f181a8f30a4c"), "Easy" },
                    { new Guid("89a57987-2e21-46d7-8a20-40fd8b3dab52"), "Hard" },
                    { new Guid("a04cf69b-9964-4cf6-b9a1-1767e6b2f406"), "Medium" }
                });

            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageUrl" },
                values: new object[,]
                {
                    { new Guid("60deef7b-d42d-4adc-85a7-248436c36ea2"), "LTPR", "Lalitpur", "images/lalitpur.png" },
                    { new Guid("6976f82d-d489-4d6a-ac79-ce834574fda8"), "BKT", "Bhaktapur", "images/Bhaktapur.png" },
                    { new Guid("75916009-756f-43da-80f4-a08506c14e85"), "NWPR", "Nawalpur", "images/nawalpur.png" },
                    { new Guid("75916009-756f-43da-80f4-a08506c14e95"), "CHTWN", "Chitwan", "images/chitwan.png" },
                    { new Guid("a4db467f-36e7-42a4-b2ee-1df41d0addb6"), "NKWT", "Nuwakot", "images/nuwakot.png" },
                    { new Guid("c4dc6da0-01b4-449b-a883-eb80540c377b"), "KTM", "Kathmandu", "images/kathmandu.png" },
                    { new Guid("d62652a9-f70c-40c8-9b63-24e7a0c945cc"), "DHG", "Dhading", "images/dhading.png" },
                    { new Guid("e87999c6-345e-4741-a109-0f09ae53a558"), "PKR", "Pokhara", "images/pokhara.png" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("335367c5-70b1-4d7f-9163-f181a8f30a4c"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("89a57987-2e21-46d7-8a20-40fd8b3dab52"));

            migrationBuilder.DeleteData(
                table: "Difficulties",
                keyColumn: "Id",
                keyValue: new Guid("a04cf69b-9964-4cf6-b9a1-1767e6b2f406"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("60deef7b-d42d-4adc-85a7-248436c36ea2"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("6976f82d-d489-4d6a-ac79-ce834574fda8"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("75916009-756f-43da-80f4-a08506c14e85"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("75916009-756f-43da-80f4-a08506c14e95"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("a4db467f-36e7-42a4-b2ee-1df41d0addb6"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("c4dc6da0-01b4-449b-a883-eb80540c377b"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d62652a9-f70c-40c8-9b63-24e7a0c945cc"));

            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("e87999c6-345e-4741-a109-0f09ae53a558"));
        }
    }
}
