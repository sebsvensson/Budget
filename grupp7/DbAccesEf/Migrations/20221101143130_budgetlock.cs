using Microsoft.EntityFrameworkCore.Migrations;

namespace DbAccesEf.Migrations
{
    public partial class budgetlock : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "BudgetLocks",
                columns: table => new
                {
                    BudgetLockID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RevenuBudgetLocked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BudgetLocks", x => x.BudgetLockID);
                });

            

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
