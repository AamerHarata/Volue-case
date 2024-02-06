using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volue_case.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Bids",
                columns: table => new
                {
                    ExternalId = table.Column<string>(type: "text", nullable: false),
                    Day = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DateOfLastChange = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Market = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Country = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bids", x => x.ExternalId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "History",
                columns: table => new
                {
                    UpdateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    BidId = table.Column<string>(type: "text", nullable: false),
                    FromStatus = table.Column<int>(type: "integer", nullable: false),
                    ToStatus = table.Column<int>(type: "integer", nullable: false),
                    BidExternalId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_History", x => new { x.BidId, x.UpdateTime });
                    table.ForeignKey(
                        name: "FK_History_Bids_BidExternalId",
                        column: x => x.BidExternalId,
                        principalTable: "Bids",
                        principalColumn: "ExternalId");
                    table.ForeignKey(
                        name: "FK_History_Bids_BidId",
                        column: x => x.BidId,
                        principalTable: "Bids",
                        principalColumn: "ExternalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Series",
                columns: table => new
                {
                    ExternalId = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Direction = table.Column<string>(type: "text", nullable: false),
                    Currency = table.Column<string>(type: "text", nullable: false),
                    PriceArea = table.Column<string>(type: "text", nullable: false),
                    AssetId = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    StartInterval = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EndInterval = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Resolution = table.Column<string>(type: "text", nullable: false),
                    BidId = table.Column<string>(type: "text", nullable: false),
                    CustomerId = table.Column<string>(type: "text", nullable: false),
                    BidExternalId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Series", x => x.ExternalId);
                    table.ForeignKey(
                        name: "FK_Series_Bids_BidExternalId",
                        column: x => x.BidExternalId,
                        principalTable: "Bids",
                        principalColumn: "ExternalId");
                    table.ForeignKey(
                        name: "FK_Series_Bids_BidId",
                        column: x => x.BidId,
                        principalTable: "Bids",
                        principalColumn: "ExternalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Series_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    SeriesId = table.Column<string>(type: "text", nullable: false),
                    SeriesExternalId = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Positions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Positions_Series_SeriesExternalId",
                        column: x => x.SeriesExternalId,
                        principalTable: "Series",
                        principalColumn: "ExternalId");
                    table.ForeignKey(
                        name: "FK_Positions_Series_SeriesId",
                        column: x => x.SeriesId,
                        principalTable: "Series",
                        principalColumn: "ExternalId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_History_BidExternalId",
                table: "History",
                column: "BidExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_SeriesExternalId",
                table: "Positions",
                column: "SeriesExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_SeriesId",
                table: "Positions",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_BidExternalId",
                table: "Series",
                column: "BidExternalId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_BidId",
                table: "Series",
                column: "BidId");

            migrationBuilder.CreateIndex(
                name: "IX_Series_CustomerId",
                table: "Series",
                column: "CustomerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "History");

            migrationBuilder.DropTable(
                name: "Positions");

            migrationBuilder.DropTable(
                name: "Series");

            migrationBuilder.DropTable(
                name: "Bids");

            migrationBuilder.DropTable(
                name: "Customers");
        }
    }
}
