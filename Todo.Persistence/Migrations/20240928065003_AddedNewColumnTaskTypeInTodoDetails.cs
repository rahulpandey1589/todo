using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedNewColumnTaskTypeInTodoDetails : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TaskType",
                table: "TodoDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TaskType",
                table: "TodoDetails");
        }
    }
}
