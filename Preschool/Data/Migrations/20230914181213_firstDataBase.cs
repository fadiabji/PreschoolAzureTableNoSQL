using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Preschool.Data.Migrations
{
    /// <inheritdoc />
    public partial class firstDataBase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Classrooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Icon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Classrooms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Childern",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EnrolDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ClassroomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Childern", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Childern_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Teachers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ClassroomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teachers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teachers_Classrooms_ClassroomId",
                        column: x => x.ClassroomId,
                        principalTable: "Classrooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentsImages",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImageFile = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChildId = table.Column<int>(type: "int", nullable: true),
                    TeacherId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentsImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DocumentsImages_Childern_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Childern",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DocumentsImages_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChildDocumentsCopies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentsCopyId = table.Column<int>(type: "int", nullable: false),
                    ChildId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChildDocumentsCopies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChildDocumentsCopies_Childern_ChildId",
                        column: x => x.ChildId,
                        principalTable: "Childern",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChildDocumentsCopies_DocumentsImages_DocumentsCopyId",
                        column: x => x.DocumentsCopyId,
                        principalTable: "DocumentsImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TeacherDocuemtnsCopies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DocumentsCopyId = table.Column<int>(type: "int", nullable: false),
                    TeacherId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherDocuemtnsCopies", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherDocuemtnsCopies_DocumentsImages_DocumentsCopyId",
                        column: x => x.DocumentsCopyId,
                        principalTable: "DocumentsImages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TeacherDocuemtnsCopies_Teachers_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Teachers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChildDocumentsCopies_ChildId",
                table: "ChildDocumentsCopies",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_ChildDocumentsCopies_DocumentsCopyId",
                table: "ChildDocumentsCopies",
                column: "DocumentsCopyId");

            migrationBuilder.CreateIndex(
                name: "IX_Childern_ClassroomId",
                table: "Childern",
                column: "ClassroomId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentsImages_ChildId",
                table: "DocumentsImages",
                column: "ChildId");

            migrationBuilder.CreateIndex(
                name: "IX_DocumentsImages_TeacherId",
                table: "DocumentsImages",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherDocuemtnsCopies_DocumentsCopyId",
                table: "TeacherDocuemtnsCopies",
                column: "DocumentsCopyId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherDocuemtnsCopies_TeacherId",
                table: "TeacherDocuemtnsCopies",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_Teachers_ClassroomId",
                table: "Teachers",
                column: "ClassroomId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChildDocumentsCopies");

            migrationBuilder.DropTable(
                name: "TeacherDocuemtnsCopies");

            migrationBuilder.DropTable(
                name: "DocumentsImages");

            migrationBuilder.DropTable(
                name: "Childern");

            migrationBuilder.DropTable(
                name: "Teachers");

            migrationBuilder.DropTable(
                name: "Classrooms");
        }
    }
}
