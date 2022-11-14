using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace RazorPages.Migrations
{
    public partial class ClientAndAnnotations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(name: "First Name", nullable: false),
                    LastName = table.Column<string>(name: "Last Name", nullable: false),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Zipcode = table.Column<string>(nullable: true),
                    Country = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: false),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    ConfirmPassword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ClientServices",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientID1 = table.Column<int>(nullable: true),
                    ServicesID1 = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientServices", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ClientServices_Clients_ClientID1",
                        column: x => x.ClientID1,
                        principalTable: "Clients",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientServices_Services_ServicesID1",
                        column: x => x.ServicesID1,
                        principalTable: "Services",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "ClientServices",
                columns: new[] { "ID", "ClientID1", "Date", "ServicesID1" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2022, 11, 7, 16, 28, 46, 578, DateTimeKind.Local).AddTicks(8253), null },
                    { 2, null, new DateTime(2022, 11, 7, 16, 28, 46, 580, DateTimeKind.Local).AddTicks(6242), null },
                    { 3, null, new DateTime(2022, 11, 7, 16, 28, 46, 580, DateTimeKind.Local).AddTicks(6274), null }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ID", "Address", "City", "ConfirmPassword", "Country", "Email", "First Name", "Last Name", "Password", "Phone", "Zipcode", "State", "Username" },
                values: new object[,]
                {
                    { 1, null, null, null, null, "flo@schmoe.net", "Flo", "Schmoe", "FloSchmoe1234*", null, null, null, "Flo" },
                    { 2, null, null, null, null, "jojo@schmoe.net", "Jo", "Schmoe", "JoJoSchmoe1234?", null, null, null, "JoJo" },
                    { 3, null, null, null, null, "truly@schmoe.net", "Truly", "Schmoe", "Truly9876**", null, null, null, "Truly" }
                });

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ID",
                keyValue: 6,
                column: "Classification",
                value: "Cut");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ID",
                keyValue: 7,
                column: "Classification",
                value: "Color");

            migrationBuilder.CreateIndex(
                name: "IX_ClientServices_ClientID1",
                table: "ClientServices",
                column: "ClientID1");

            migrationBuilder.CreateIndex(
                name: "IX_ClientServices_ServicesID1",
                table: "ClientServices",
                column: "ServicesID1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientServices");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ID",
                keyValue: 6,
                column: "Classification",
                value: "Salon");

            migrationBuilder.UpdateData(
                table: "Services",
                keyColumn: "ID",
                keyValue: 7,
                column: "Classification",
                value: "Salon");
        }
    }
}
