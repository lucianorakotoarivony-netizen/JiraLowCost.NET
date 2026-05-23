using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JiraLowCost.api.Migrations
{
    /// <inheritdoc />
    public partial class CK : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "TaskItems",
                type: "text",
                nullable: false,
                defaultValue: "TODO",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "A faire");

            migrationBuilder.AlterColumn<string>(
                name: "Priority",
                table: "TaskItems",
                type: "text",
                nullable: false,
                defaultValue: "MEDIUM",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "Moyenne");

            migrationBuilder.AddCheckConstraint(
                name: "CK_TaskItems_Difficulty",
                table: "TaskItems",
                sql: "\"Difficulty\" IN ('JUNIOR', 'MID', 'SENIOR', 'LEAD')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_TaskItems_Priority",
                table: "TaskItems",
                sql: "\"Priority\" IN ('LOW', 'MEDIUM', 'HIGH')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_TaskItems_Status",
                table: "TaskItems",
                sql: "\"Status\" IN ('TODO', 'IN_PROGRESS', 'PENDING', 'DONE')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_user_role",
                table: "AspNetUsers",
                sql: "\"Role\" IN ('JUNIOR', 'MID', 'SENIOR', 'LEAD', 'PO')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_TaskItems_Difficulty",
                table: "TaskItems");

            migrationBuilder.DropCheckConstraint(
                name: "CK_TaskItems_Priority",
                table: "TaskItems");

            migrationBuilder.DropCheckConstraint(
                name: "CK_TaskItems_Status",
                table: "TaskItems");

            migrationBuilder.DropCheckConstraint(
                name: "CK_user_role",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Status",
                table: "TaskItems",
                type: "text",
                nullable: false,
                defaultValue: "A faire",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "TODO");

            migrationBuilder.AlterColumn<string>(
                name: "Priority",
                table: "TaskItems",
                type: "text",
                nullable: false,
                defaultValue: "Moyenne",
                oldClrType: typeof(string),
                oldType: "text",
                oldDefaultValue: "MEDIUM");
        }
    }
}
