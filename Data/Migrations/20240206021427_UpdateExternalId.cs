using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Volue_case.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateExternalId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_Bids_BidExternalId",
                table: "History");

            migrationBuilder.DropForeignKey(
                name: "FK_History_Bids_BidId",
                table: "History");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Series_SeriesExternalId",
                table: "Positions");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Series_SeriesId",
                table: "Positions");

            migrationBuilder.DropForeignKey(
                name: "FK_Series_Bids_BidExternalId",
                table: "Series");

            migrationBuilder.DropForeignKey(
                name: "FK_Series_Bids_BidId",
                table: "Series");

            migrationBuilder.DropIndex(
                name: "IX_Series_BidId",
                table: "Series");

            migrationBuilder.DropIndex(
                name: "IX_Positions_SeriesId",
                table: "Positions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_History",
                table: "History");

            migrationBuilder.DropIndex(
                name: "IX_History_BidExternalId",
                table: "History");

            migrationBuilder.DropColumn(
                name: "BidId",
                table: "Series");

            migrationBuilder.DropColumn(
                name: "SeriesId",
                table: "Positions");

            migrationBuilder.DropColumn(
                name: "BidId",
                table: "History");

            migrationBuilder.AlterColumn<string>(
                name: "BidExternalId",
                table: "Series",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BidExternalId1",
                table: "Series",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SeriesExternalId",
                table: "Positions",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SeriesExternalId1",
                table: "Positions",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BidExternalId",
                table: "History",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BidExternalId1",
                table: "History",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_History",
                table: "History",
                columns: new[] { "BidExternalId", "UpdateTime" });

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
                name: "FK_History_Bids_BidExternalId",
                table: "History",
                column: "BidExternalId",
                principalTable: "Bids",
                principalColumn: "ExternalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_History_Bids_BidExternalId1",
                table: "History",
                column: "BidExternalId1",
                principalTable: "Bids",
                principalColumn: "ExternalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Series_SeriesExternalId",
                table: "Positions",
                column: "SeriesExternalId",
                principalTable: "Series",
                principalColumn: "ExternalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Series_SeriesExternalId1",
                table: "Positions",
                column: "SeriesExternalId1",
                principalTable: "Series",
                principalColumn: "ExternalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Bids_BidExternalId",
                table: "Series",
                column: "BidExternalId",
                principalTable: "Bids",
                principalColumn: "ExternalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Bids_BidExternalId1",
                table: "Series",
                column: "BidExternalId1",
                principalTable: "Bids",
                principalColumn: "ExternalId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_History_Bids_BidExternalId",
                table: "History");

            migrationBuilder.DropForeignKey(
                name: "FK_History_Bids_BidExternalId1",
                table: "History");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Series_SeriesExternalId",
                table: "Positions");

            migrationBuilder.DropForeignKey(
                name: "FK_Positions_Series_SeriesExternalId1",
                table: "Positions");

            migrationBuilder.DropForeignKey(
                name: "FK_Series_Bids_BidExternalId",
                table: "Series");

            migrationBuilder.DropForeignKey(
                name: "FK_Series_Bids_BidExternalId1",
                table: "Series");

            migrationBuilder.DropIndex(
                name: "IX_Series_BidExternalId1",
                table: "Series");

            migrationBuilder.DropIndex(
                name: "IX_Positions_SeriesExternalId1",
                table: "Positions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_History",
                table: "History");

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

            migrationBuilder.AlterColumn<string>(
                name: "BidExternalId",
                table: "Series",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "BidId",
                table: "Series",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "SeriesExternalId",
                table: "Positions",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "SeriesId",
                table: "Positions",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "BidExternalId",
                table: "History",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<string>(
                name: "BidId",
                table: "History",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_History",
                table: "History",
                columns: new[] { "BidId", "UpdateTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Series_BidId",
                table: "Series",
                column: "BidId");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_SeriesId",
                table: "Positions",
                column: "SeriesId");

            migrationBuilder.CreateIndex(
                name: "IX_History_BidExternalId",
                table: "History",
                column: "BidExternalId");

            migrationBuilder.AddForeignKey(
                name: "FK_History_Bids_BidExternalId",
                table: "History",
                column: "BidExternalId",
                principalTable: "Bids",
                principalColumn: "ExternalId");

            migrationBuilder.AddForeignKey(
                name: "FK_History_Bids_BidId",
                table: "History",
                column: "BidId",
                principalTable: "Bids",
                principalColumn: "ExternalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Series_SeriesExternalId",
                table: "Positions",
                column: "SeriesExternalId",
                principalTable: "Series",
                principalColumn: "ExternalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Positions_Series_SeriesId",
                table: "Positions",
                column: "SeriesId",
                principalTable: "Series",
                principalColumn: "ExternalId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Bids_BidExternalId",
                table: "Series",
                column: "BidExternalId",
                principalTable: "Bids",
                principalColumn: "ExternalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Series_Bids_BidId",
                table: "Series",
                column: "BidId",
                principalTable: "Bids",
                principalColumn: "ExternalId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
