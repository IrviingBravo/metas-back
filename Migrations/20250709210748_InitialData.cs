using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace metas.Migrations
{
    /// <inheritdoc />
    public partial class InitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_ActivityLists_ActivityListId",
                table: "TaskItems");

            migrationBuilder.DropIndex(
                name: "IX_TaskItems_ActivityListId",
                table: "TaskItems");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "TaskItems",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "DateCreation",
                table: "ActivityLists",
                newName: "CreatedAt");

            migrationBuilder.AddColumn<int>(
                name: "ActivityListMigrationId",
                table: "TaskItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "TaskItems",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_ActivityListMigrationId",
                table: "TaskItems",
                column: "ActivityListMigrationId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_ActivityLists_ActivityListMigrationId",
                table: "TaskItems",
                column: "ActivityListMigrationId",
                principalTable: "ActivityLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TaskItems_ActivityLists_ActivityListMigrationId",
                table: "TaskItems");

            migrationBuilder.DropIndex(
                name: "IX_TaskItems_ActivityListMigrationId",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "ActivityListMigrationId",
                table: "TaskItems");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "TaskItems");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "TaskItems",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "ActivityLists",
                newName: "DateCreation");

            migrationBuilder.CreateIndex(
                name: "IX_TaskItems_ActivityListId",
                table: "TaskItems",
                column: "ActivityListId");

            migrationBuilder.AddForeignKey(
                name: "FK_TaskItems_ActivityLists_ActivityListId",
                table: "TaskItems",
                column: "ActivityListId",
                principalTable: "ActivityLists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
