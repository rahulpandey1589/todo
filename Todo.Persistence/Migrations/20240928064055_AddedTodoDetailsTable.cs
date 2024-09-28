using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Todo.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedTodoDetailsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TodoDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TodoId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoDetails_Todos_TodoId",
                        column: x => x.TodoId,
                        principalTable: "Todos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TodoDetails_TodoId",
                table: "TodoDetails",
                column: "TodoId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TodoDetails");
        }
    }
}
