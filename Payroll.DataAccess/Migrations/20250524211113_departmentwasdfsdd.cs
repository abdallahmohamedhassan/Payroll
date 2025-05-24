using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Payroll.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class departmentwasdfsdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_ExperienceIncentives_GreaterThanYear",
                table: "ExperienceIncentives",
                column: "GreaterThanYear",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_ExperienceIncentives_GreaterThanYear",
                table: "ExperienceIncentives");
        }
    }
}
