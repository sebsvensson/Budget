using Microsoft.EntityFrameworkCore.Migrations;

namespace DbAccesEf.Migrations
{
    public partial class test1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AccountNumber = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    SchablonExpense = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                });

            migrationBuilder.CreateTable(
                name: "Activities",
                columns: table => new
                {
                    ActivityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomID = table.Column<string>(nullable: true),
                    ActivityXxxx = table.Column<string>(nullable: true),
                    ActivityName = table.Column<string>(nullable: true),
                    AFFODepartment = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Activities", x => x.ActivityID);
                });

            migrationBuilder.CreateTable(
                name: "Personells",
                columns: table => new
                {
                    PersonellID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Pnr = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    MonthlySalary = table.Column<double>(nullable: false),
                    EmploymentRate = table.Column<double>(nullable: false),
                    VacancyDeduction = table.Column<double>(nullable: false),
                    AnnualWorkRate = table.Column<double>(nullable: false),
                    Adm = table.Column<double>(nullable: false),
                    ForsMark = table.Column<double>(nullable: false),
                    UtvForv = table.Column<double>(nullable: false),
                    Drift = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Personells", x => x.PersonellID);
                });

            migrationBuilder.CreateTable(
                name: "ProductAllocation",
                columns: table => new
                {
                    ProductAllocationID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonellID = table.Column<int>(nullable: false),
                    Allocation = table.Column<double>(nullable: false),
                    ProductID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAllocation", x => x.ProductAllocationID);
                    table.ForeignKey(
                        name: "FK_ProductAllocation_Personells_PersonellID",
                        column: x => x.PersonellID,
                        principalTable: "Personells",
                        principalColumn: "PersonellID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAllocation_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAllocation_PersonellID",
                table: "ProductAllocation",
                column: "PersonellID");

            migrationBuilder.CreateIndex(
                name: "IX_ProductAllocation_ProductID",
                table: "ProductAllocation",
                column: "ProductID");



        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
