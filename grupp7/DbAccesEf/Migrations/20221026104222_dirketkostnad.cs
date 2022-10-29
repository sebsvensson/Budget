using Microsoft.EntityFrameworkCore.Migrations;

namespace DbAccesEf.Migrations
{
    public partial class dirketkostnad : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            

            migrationBuilder.CreateTable(
                name: "DirectCostActivities",
                columns: table => new
                {
                    DirectCostActivityID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<double>(nullable: false),
                    ActivityID = table.Column<int>(nullable: true),
                    AccountId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectCostActivities", x => x.DirectCostActivityID);
                    table.ForeignKey(
                        name: "FK_DirectCostActivities_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectCostActivities_Activities_ActivityID",
                        column: x => x.ActivityID,
                        principalTable: "Activities",
                        principalColumn: "ActivityID",
                        onDelete: ReferentialAction.Restrict);
                });

            

            migrationBuilder.CreateTable(
                name: "DirectCostProducts",
                columns: table => new
                {
                    DirectCostProductID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Cost = table.Column<double>(nullable: false),
                    ProductID = table.Column<int>(nullable: true),
                    AccountId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectCostProducts", x => x.DirectCostProductID);
                    table.ForeignKey(
                        name: "FK_DirectCostProducts_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "AccountId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DirectCostProducts_Products_ProductID",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "ProductID",
                        onDelete: ReferentialAction.Restrict);
                });

            

            

            migrationBuilder.CreateIndex(
                name: "IX_DirectCostActivities_AccountId",
                table: "DirectCostActivities",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCostActivities_ActivityID",
                table: "DirectCostActivities",
                column: "ActivityID");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCostProducts_AccountId",
                table: "DirectCostProducts",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectCostProducts_ProductID",
                table: "DirectCostProducts",
                column: "ProductID");

            
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
