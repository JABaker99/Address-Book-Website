using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable CA1814

/// <summary>
/// Baker Address Book - Initial Migration
/// Author: Jacob Baker
/// Created: 2025-09-21
/// Description:
/// This migration creates the initial database schema for the Baker Address Book application.
/// It sets up the Categories and Contacts tables, defines their relationships,
/// and seeds initial data for both tables.
/// </summary>
namespace BakerAddressBook.Migrations
{
    /// <summary>
    /// Initial migration class for creating the database schema.
    /// </summary>
    public partial class Initial : Migration
    {
        /// <summary>
        /// Applies the migration to create tables and seed initial data.
        /// </summary>
        /// <param name="migrationBuilder">Builder used to configure database schema changes.</param>
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                columns: table => new
                {
                    ContactId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Nickname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ContactId);
                    table.ForeignKey(
                        name: "FK_Contacts_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Family" },
                    { 2, "Friend" },
                    { 3, "Work" }
                });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "ContactId", "CategoryId", "DateCreated", "FirstName", "LastName", "Nickname", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, 2, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Alice", "Kelly", "Ally", "123-456-7890" },
                    { 2, 1, new DateTime(2025, 2, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bob", "Baker", null, "770-423-6789 " },
                    { 3, 3, new DateTime(2025, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Charlie", "Baker", "Chuck", "476-543-2211" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CategoryId",
                table: "Contacts",
                column: "CategoryId");
        }

        /// <summary>
        /// Reverts the migration by dropping the created tables.
        /// </summary>
        /// <param name="migrationBuilder">Builder used to undo database schema changes.</param>
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contacts");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
