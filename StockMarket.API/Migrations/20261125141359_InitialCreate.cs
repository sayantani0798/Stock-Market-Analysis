using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockMarket.API.Migrations;

/// <inheritdoc />
public partial class _20261125141359_InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Stocks",
            columns: table => new
            {
                StockId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Symbol = table.Column<string>(type: "nvarchar(450)", nullable: false),
                CompanyName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Exchange = table.Column<string>(type: "nvarchar(450)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Stocks", x => x.StockId);
            });

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                UserId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.UserId);
            });

        migrationBuilder.CreateTable(
            name: "PriceHistories",
            columns: table => new
            {
                PriceHistoryId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                StockId = table.Column<int>(type: "int", nullable: false),
                Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                Open = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                High = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Low = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                Close = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                Volume = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_PriceHistories", x => x.PriceHistoryId);
                table.ForeignKey(
                    name: "FK_PriceHistories_Stocks_StockId",
                    column: x => x.StockId,
                    principalTable: "Stocks",
                    principalColumn: "StockId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Portfolios",
            columns: table => new
            {
                PortfolioId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Portfolios", x => x.PortfolioId);
                table.ForeignKey(
                    name: "FK_Portfolios_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Watchlists",
            columns: table => new
            {
                WatchlistId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                UserId = table.Column<int>(type: "int", nullable: false),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Watchlists", x => x.WatchlistId);
                table.ForeignKey(
                    name: "FK_Watchlists_Users_UserId",
                    column: x => x.UserId,
                    principalTable: "Users",
                    principalColumn: "UserId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "Transactions",
            columns: table => new
            {
                TransactionId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                PortfolioId = table.Column<int>(type: "int", nullable: false),
                StockId = table.Column<int>(type: "int", nullable: false),
                TransactionType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                Quantity = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                Price = table.Column<decimal>(type: "decimal(18,4)", precision: 18, scale: 4, nullable: false),
                TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                table.ForeignKey(
                    name: "FK_Transactions_Portfolios_PortfolioId",
                    column: x => x.PortfolioId,
                    principalTable: "Portfolios",
                    principalColumn: "PortfolioId",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Transactions_Stocks_StockId",
                    column: x => x.StockId,
                    principalTable: "Stocks",
                    principalColumn: "StockId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "WatchlistItems",
            columns: table => new
            {
                WatchlistItemId = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                WatchlistId = table.Column<int>(type: "int", nullable: false),
                StockId = table.Column<int>(type: "int", nullable: false),
                AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_WatchlistItems", x => x.WatchlistItemId);
                table.ForeignKey(
                    name: "FK_WatchlistItems_Stocks_StockId",
                    column: x => x.StockId,
                    principalTable: "Stocks",
                    principalColumn: "StockId",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_WatchlistItems_Watchlists_WatchlistId",
                    column: x => x.WatchlistId,
                    principalTable: "Watchlists",
                    principalColumn: "WatchlistId",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Portfolios_UserId",
            table: "Portfolios",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_PriceHistories_StockId_Date",
            table: "PriceHistories",
            columns: new[] { "StockId", "Date" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Stocks_Symbol_Exchange",
            table: "Stocks",
            columns: new[] { "Symbol", "Exchange" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Transactions_PortfolioId",
            table: "Transactions",
            column: "PortfolioId");

        migrationBuilder.CreateIndex(
            name: "IX_Transactions_StockId",
            table: "Transactions",
            column: "StockId");

        migrationBuilder.CreateIndex(
            name: "IX_Users_Email",
            table: "Users",
            column: "Email",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_WatchlistItems_StockId",
            table: "WatchlistItems",
            column: "StockId");

        migrationBuilder.CreateIndex(
            name: "IX_WatchlistItems_WatchlistId_StockId",
            table: "WatchlistItems",
            columns: new[] { "WatchlistId", "StockId" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Watchlists_UserId",
            table: "Watchlists",
            column: "UserId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "PriceHistories");

        migrationBuilder.DropTable(
            name: "Transactions");

        migrationBuilder.DropTable(
            name: "WatchlistItems");

        migrationBuilder.DropTable(
            name: "Portfolios");

        migrationBuilder.DropTable(
            name: "Stocks");

        migrationBuilder.DropTable(
            name: "Watchlists");

        migrationBuilder.DropTable(
            name: "Users");
    }
}
