using Microsoft.EntityFrameworkCore.Migrations;

namespace YTubers.Web.Data.Migrations
{
    public partial class RemovingDuplicateSubCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SubscriberCount",
                table: "YouTubers");

            migrationBuilder.DropColumn(
                name: "Subscribers",
                table: "YouTubers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "SubscriberCount",
                table: "YouTubers",
                type: "decimal(20,0)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<long>(
                name: "Subscribers",
                table: "YouTubers",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
