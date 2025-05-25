using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class NewMigrationNamenew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Attendnce_GreaterThanDays",
                table: "Attendnce",
                column: "GreaterThanDays",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Attendnce_GreaterThanDays",
                table: "Attendnce");
        }
    }
}
