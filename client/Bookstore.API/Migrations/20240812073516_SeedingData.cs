using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Bookstore.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedingData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Authors",
                columns: new[] { "AuthorId", "Biography", "ImageUrl", "Name" },
                values: new object[,]
                {
                    { 9, "biography to be written", "/images/authors/imagesunavailable.jpg", "Author 2024" },
                    { 16, "coming soon", "/images/authors/imagesunavailable.jpg", "Author 2025" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "AuthorId", "GenreId", "ImageUrl", "Price", "PublicationDate", "Title" },
                values: new object[,]
                {
                    { 6, 4, 7, "/images/books/imageunavailable.jpg", 899.99m, new DateOnly(2008, 1, 1), "Book title" },
                    { 7, 5, 3, "/images/books/imageunavailable.jpg", 99.99m, new DateOnly(2022, 1, 1), "Book Title2" }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "GenreId", "GenreName", "ImageUrl" },
                values: new object[,]
                {
                    { 9, "Tragedy", "/images/genres/genreimageunavailable.jpg" },
                    { 10, "Theatre", "/images/genres/genreimageunavailable.jpg" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Authors",
                keyColumn: "AuthorId",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "GenreId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Genres",
                keyColumn: "GenreId",
                keyValue: 10);
        }
    }
}
