using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volue_case.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemoveForeignKeyDuplication : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_Bids_BidExternalId1",
                table: "History");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Series_SeriesExternalId1",
                table: "Positions");

            migrationBuilder.DropForeignKey(
                name: "FK_Series_Bids_BidExternalId1",
                table: "Series");

            migrationBuilder.DropIndex(
                name: "IX_Series_BidExternalId1",
                table: "Series");

            migrationBuilder.DropIndex(
                name: "IX_Positions_SeriesExternalId1",
                table: "Positions");

            migrationBuilder.DropIndex(
                name: "IX_History_BidExternalId1",
                table: "History");

            migrationBuilder.DropColumn(
                name: "BidExternalId1",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "SeriesExternalId1",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "BidExternalId1",
                table: "History");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BidExternalId1",
                table: "Series",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeriesExternalId1",
                table: "Positions",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BidExternalId1",
                table: "History",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Series_BidExternalId1",
                table: "Series",
                column: "BidExternalId1");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_SeriesExternalId1",
                table: "Positions",
                column: "SeriesExternalId1");

            migrationBuilder.CreateIndex(
                name: "IX_History_BidExternalId1",
                table: "History",
                column: "BidExternalId1");

            migrationBuilder.AddForeignKey(
                name: "FK_History_Bids_BidExternalId1",
                table: "History",
                column: "BidExternalId1",
                principalTable: "Bids",
                principalColumn: "ExternalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Series_SeriesExternalId1",
                table: "Positions",
                column: "SeriesExternalId1",
                principalTable: "Series",
                principalColumn: "ExternalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Bids_BidExternalId1",
                table: "Series",
                column: "BidExternalId1",
                principalTable: "Bids",
                principalColumn: "ExternalId");
        }
    }
}
