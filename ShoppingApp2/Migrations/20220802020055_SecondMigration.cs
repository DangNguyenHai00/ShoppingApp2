using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingApp2.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Receipts_ReceiptId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_AspNetUsers_UsernameId",
                table: "Receipts");

            migrationBuilder.DropIndex(
                name: "IX_Items_ReceiptId",
                table: "Items");

            migrationBuilder.DropColumn(
                name: "ReceiptId",
                table: "Items");

            migrationBuilder.RenameColumn(
                name: "UsernameId",
                table: "Receipts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_UsernameId",
                table: "Receipts",
                newName: "IX_Receipts_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_AspNetUsers_UserId",
                table: "Receipts",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Receipts_AspNetUsers_UserId",
                table: "Receipts");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Receipts",
                newName: "UsernameId");

            migrationBuilder.RenameIndex(
                name: "IX_Receipts_UserId",
                table: "Receipts",
                newName: "IX_Receipts_UsernameId");

            migrationBuilder.AddColumn<int>(
                name: "ReceiptId",
                table: "Items",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_ReceiptId",
                table: "Items",
                column: "ReceiptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Receipts_ReceiptId",
                table: "Items",
                column: "ReceiptId",
                principalTable: "Receipts",
                principalColumn: "ReceiptId");

            migrationBuilder.AddForeignKey(
                name: "FK_Receipts_AspNetUsers_UsernameId",
                table: "Receipts",
                column: "UsernameId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
