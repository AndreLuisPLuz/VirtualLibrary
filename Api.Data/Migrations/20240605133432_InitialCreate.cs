using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Api.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    PagesCount = table.Column<int>(type: "int", nullable: true),
                    LendingOption = table.Column<int>(type: "int", nullable: false),
                    AvailableQuantity = table.Column<int>(type: "int", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuthorBook",
                columns: table => new
                {
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorBook", x => new { x.AuthorId, x.BookId });
                    table.ForeignKey(
                        name: "FK_AuthorBook_Authors_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorBook_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookGender",
                columns: table => new
                {
                    GenderId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookGender", x => new { x.GenderId, x.BookId });
                    table.ForeignKey(
                        name: "FK_BookGender_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookGender_Genders_GenderId",
                        column: x => x.GenderId,
                        principalTable: "Genders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lendings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StartingAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndingAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DevolutionAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FineApplied = table.Column<double>(type: "float", nullable: true),
                    FinePaidAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    BookId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lendings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lendings_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Lendings_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "Id", "CreatedAt", "Gender", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("0d1fddb4-381c-47ef-a24f-2bfc3a8f4ea0"), new DateTime(2024, 6, 5, 13, 34, 32, 27, DateTimeKind.Utc).AddTicks(3398), 1, "J. K. Rowling", null },
                    { new Guid("91945ac8-f057-4fff-9ea1-939f10666ab6"), new DateTime(2024, 6, 5, 13, 34, 32, 27, DateTimeKind.Utc).AddTicks(3394), 0, "H. P. Lovecraft", null }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "AvailableQuantity", "CreatedAt", "Description", "ISBN", "LendingOption", "PagesCount", "Title", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("52067d00-fdc9-41fe-9e88-e3ec899f9286"), null, new DateTime(2024, 6, 5, 13, 34, 32, 27, DateTimeKind.Utc).AddTicks(3658), "Dreams point to the return of an old horror.", "9789583067341", 0, 179, "The Call of Cthulhu", null },
                    { new Guid("b31770e7-5655-4d1a-8d54-06cb31b01544"), null, new DateTime(2024, 6, 5, 13, 34, 32, 27, DateTimeKind.Utc).AddTicks(3655), "A boy learns how to do magic.", "9781781100349", 0, 278, "Harry Potter & the Philosopher's Stone", null }
                });

            migrationBuilder.InsertData(
                table: "Genders",
                columns: new[] { "Id", "CreatedAt", "Name", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("16624fa0-90ae-4fc0-85f6-3f644ab5e5f9"), new DateTime(2024, 6, 5, 13, 34, 32, 27, DateTimeKind.Utc).AddTicks(3618), "Thriller", null },
                    { new Guid("1a900293-d703-43b7-8658-50f3ac3dff4a"), new DateTime(2024, 6, 5, 13, 34, 32, 27, DateTimeKind.Utc).AddTicks(3615), "Fantasy", null },
                    { new Guid("be487b42-4952-4015-b122-ed03bbc8b8e3"), new DateTime(2024, 6, 5, 13, 34, 32, 27, DateTimeKind.Utc).AddTicks(3613), "Adventure", null },
                    { new Guid("e3860499-a30e-4963-ab35-487a1215ea20"), new DateTime(2024, 6, 5, 13, 34, 32, 27, DateTimeKind.Utc).AddTicks(3617), "Horror", null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Gender", "Name", "Password", "UpdatedAt" },
                values: new object[,]
                {
                    { new Guid("c3da35f9-7e4f-449d-b617-598161541d3c"), new DateTime(2024, 6, 5, 13, 34, 32, 32, DateTimeKind.Utc).AddTicks(7048), "andreluispluz@gmail.com", 0, "André", "AQAAAAIAAYagAAAAEKMwjrz2ndtXDY84TeWyv55GmpiqTEi1XPArEoyHaGBmpxOMwMxvkT3cLuC4gbEFCg==", null },
                    { new Guid("c684a7b2-796e-4248-96ed-96a658f3535b"), new DateTime(2024, 6, 5, 13, 34, 32, 116, DateTimeKind.Utc).AddTicks(6993), "mateus@gmail.com", 0, "Mateus", "AQAAAAIAAYagAAAAENvOhyKVz21lq4VteHFxVcvvt0ZVvXtskkh8p6sFaRP/xLT+jwPm6JrJ0Id+O/oPEg==", null }
                });

            migrationBuilder.InsertData(
                table: "AuthorBook",
                columns: new[] { "AuthorId", "BookId" },
                values: new object[,]
                {
                    { new Guid("0d1fddb4-381c-47ef-a24f-2bfc3a8f4ea0"), new Guid("b31770e7-5655-4d1a-8d54-06cb31b01544") },
                    { new Guid("91945ac8-f057-4fff-9ea1-939f10666ab6"), new Guid("52067d00-fdc9-41fe-9e88-e3ec899f9286") }
                });

            migrationBuilder.InsertData(
                table: "BookGender",
                columns: new[] { "BookId", "GenderId" },
                values: new object[,]
                {
                    { new Guid("52067d00-fdc9-41fe-9e88-e3ec899f9286"), new Guid("16624fa0-90ae-4fc0-85f6-3f644ab5e5f9") },
                    { new Guid("b31770e7-5655-4d1a-8d54-06cb31b01544"), new Guid("1a900293-d703-43b7-8658-50f3ac3dff4a") },
                    { new Guid("b31770e7-5655-4d1a-8d54-06cb31b01544"), new Guid("be487b42-4952-4015-b122-ed03bbc8b8e3") },
                    { new Guid("52067d00-fdc9-41fe-9e88-e3ec899f9286"), new Guid("e3860499-a30e-4963-ab35-487a1215ea20") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorBook_BookId",
                table: "AuthorBook",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BookGender_BookId",
                table: "BookGender",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Lendings_BookId",
                table: "Lendings",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Lendings_UserId",
                table: "Lendings",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorBook");

            migrationBuilder.DropTable(
                name: "BookGender");

            migrationBuilder.DropTable(
                name: "Lendings");

            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Genders");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
