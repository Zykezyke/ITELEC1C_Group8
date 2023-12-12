using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITELEC1C_Group8.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAppointmentTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

                    // Drop existing Appointments table if it exists
        migrationBuilder.Sql("DROP TABLE IF EXISTS [Appointments]");
            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    AppointmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AUserName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AFullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    APhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SelectedBranch = table.Column<int>(type: "int", nullable: false),
                    SelectedDoctor = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AppDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    DoctorNotes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.AppointmentId);
                });

         

            migrationBuilder.AddColumn<int>(
    name: "Status",
    table: "Appointments",
    type: "int",
    nullable: false,
    defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DoctorNotes",
                table: "Appointments",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "No notes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

        }
    }
}
