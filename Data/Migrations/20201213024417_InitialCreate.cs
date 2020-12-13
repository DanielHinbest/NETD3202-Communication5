using Microsoft.EntityFrameworkCore.Migrations;

namespace NETD3202_Communication5.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    scoreID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    teamOneTotal = table.Column<int>(nullable: false),
                    teamTwoTotal = table.Column<int>(nullable: false),
                    teamOneEnds = table.Column<int>(nullable: false),
                    teamTwoEnds = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.scoreID);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    teamID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    leadName = table.Column<string>(nullable: true),
                    secondName = table.Column<string>(nullable: true),
                    viceName = table.Column<string>(nullable: true),
                    skipName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.teamID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "Teams");
        }
    }
}
