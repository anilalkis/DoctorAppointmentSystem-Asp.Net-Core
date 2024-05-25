using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DoctorAppointmentSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class mig_update_DaysOffTbl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_DaysOff_DoctorId",
                table: "DaysOff",
                column: "DoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_DaysOff_Doctors_DoctorId",
                table: "DaysOff",
                column: "DoctorId",
                principalTable: "Doctors",
                principalColumn: "DoctorId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DaysOff_Doctors_DoctorId",
                table: "DaysOff");

            migrationBuilder.DropIndex(
                name: "IX_DaysOff_DoctorId",
                table: "DaysOff");
        }
    }
}
