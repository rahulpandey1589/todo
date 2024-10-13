using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemovedFetchTodoResultFromContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FetchTodoResult");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FetchTodoResult",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssignedTo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsCompleted = table.Column<bool>(type: "bit", nullable: false),
                    TaskName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TaskType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FetchTodoResult", x => x.Id);
                });
        }
    }
}
