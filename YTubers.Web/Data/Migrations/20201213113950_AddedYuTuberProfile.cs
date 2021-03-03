using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YTubers.Web.Data.Migrations
{
    public partial class AddedYuTuberProfile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "YouTubers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChannelName = table.Column<string>(maxLength: 50, nullable: false),
                    ChannelLink = table.Column<string>(maxLength: 150, nullable: false),
                    CategoryId = table.Column<int>(nullable: false),
                    City = table.Column<string>(maxLength: 50, nullable: false),
                    Age = table.Column<int>(nullable: false, defaultValue: 0),
                    Subscribers = table.Column<long>(nullable: false),
                    IsFeatured = table.Column<bool>(nullable: false, defaultValue: false),
                    Price = table.Column<double>(nullable: false),
                    AppUserId = table.Column<string>(nullable: false),
                    ChannelId = table.Column<string>(nullable: true),
                    SubscribersCount = table.Column<decimal>(nullable: false),
                    ThumbnailUrl = table.Column<string>(nullable: true),
                    SubscriberCount = table.Column<decimal>(nullable: false),
                    VideoCount = table.Column<decimal>(nullable: false),
                    YourDescription = table.Column<string>(maxLength: 500, nullable: true),
                    UserSince = table.Column<DateTime>(nullable: false),
                    Sex = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_YouTubers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_YouTubers_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_YouTubers_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_YouTubers_AppUserId",
                table: "YouTubers",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_YouTubers_CategoryId",
                table: "YouTubers",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "YouTubers");
        }
    }
}
