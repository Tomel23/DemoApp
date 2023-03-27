using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Demo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    AccessToken = table.Column<string>(type: "TEXT", nullable: true),
                    TokenType = table.Column<string>(type: "TEXT", nullable: true),
                    ExpiresIn = table.Column<long>(type: "INTEGER", nullable: false),
                    Scope = table.Column<string>(type: "TEXT", nullable: true),
                    CreationDate = table.Column<DateTimeOffset>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbTokens", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbTokens");
        }
    }
}
