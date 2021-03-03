using Microsoft.EntityFrameworkCore.Migrations;

namespace YTubers.Web.Data.Migrations
{
    public partial class AddedReachOutForm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReachRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(maxLength: 50, nullable: false),
                    Email = table.Column<string>(maxLength: 50, nullable: false),
                    City = table.Column<string>(maxLength: 30, nullable: false),
                    State = table.Column<string>(maxLength: 30, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 20, nullable: false),
                    Message = table.Column<string>(maxLength: 250, nullable: false),
                    YuTuberUserId = table.Column<string>(nullable: false),
                    YuTuberId = table.Column<int>(nullable: false),
                    AppUserId = table.Column<string>(nullable: false),
                    RequestStatus = table.Column<int>(nullable: false,defaultValue:0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReachRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReachRequests_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_ReachRequests_YouTubers_YuTuberId",
                        column: x => x.YuTuberId,
                        principalTable: "YouTubers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReachRequests_AppUserId",
                table: "ReachRequests",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ReachRequests_YuTuberId",
                table: "ReachRequests",
                column: "YuTuberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReachRequests");
        }
    }
}
