using Microsoft.EntityFrameworkCore.Migrations;

namespace RoskhTest.Data.Migrations
{
    public partial class SeedPackageStates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "PackageStates",
                columns: new[] { "Id", "Code", "Description", "Name" },
                values: new object[] { 1L, "WfPU", "Csomag a feladónál. Futárra vár.", "Waiting for Pick Up" });

            migrationBuilder.InsertData(
                table: "PackageStates",
                columns: new[] { "Id", "Code", "Description", "Name" },
                values: new object[] { 2L, "PU", "Csomag a futárnál. Depóba tart.", "Picked Up" });

            migrationBuilder.InsertData(
                table: "PackageStates",
                columns: new[] { "Id", "Code", "Description", "Name" },
                values: new object[] { 3L, "ID", "Depóban van. Kiszállításra vár.", "In Depo" });

            migrationBuilder.InsertData(
                table: "PackageStates",
                columns: new[] { "Id", "Code", "Description", "Name" },
                values: new object[] { 4L, "OD", "Kiszállítás alatt. Célba tart.", "On Delivery" });

            migrationBuilder.InsertData(
                table: "PackageStates",
                columns: new[] { "Id", "Code", "Description", "Name" },
                values: new object[] { 5L, "DD", "Kiszállítva.", "Delivered" });

            migrationBuilder.CreateIndex(
                name: "IX_Packages_StateId",
                table: "Packages",
                column: "StateId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM PackageStates", true);
        }
    }
}
