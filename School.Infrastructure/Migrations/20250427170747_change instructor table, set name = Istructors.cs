using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace School.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class changeinstructortablesetnameIstructors : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Instructor_SupervisorId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Departments_DID",
                table: "Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructor_Instructor_SupervisorId",
                table: "Instructor");

            migrationBuilder.DropForeignKey(
                name: "FK_InstructorSubject_Instructor_InsID",
                table: "InstructorSubject");

            migrationBuilder.DropForeignKey(
                name: "FK_InstructorSubject_Subjects_SubID",
                table: "InstructorSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstructorSubject",
                table: "InstructorSubject");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instructor",
                table: "Instructor");

            migrationBuilder.RenameTable(
                name: "InstructorSubject",
                newName: "InstructorSubjects");

            migrationBuilder.RenameTable(
                name: "Instructor",
                newName: "Instructors");

            migrationBuilder.RenameIndex(
                name: "IX_InstructorSubject_InsID",
                table: "InstructorSubjects",
                newName: "IX_InstructorSubjects_InsID");

            migrationBuilder.RenameIndex(
                name: "IX_Instructor_SupervisorId",
                table: "Instructors",
                newName: "IX_Instructors_SupervisorId");

            migrationBuilder.RenameIndex(
                name: "IX_Instructor_DID",
                table: "Instructors",
                newName: "IX_Instructors_DID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstructorSubjects",
                table: "InstructorSubjects",
                columns: new[] { "SubID", "InsID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instructors",
                table: "Instructors",
                column: "InsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Instructors_SupervisorId",
                table: "Departments",
                column: "SupervisorId",
                principalTable: "Instructors",
                principalColumn: "InsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Departments_DID",
                table: "Instructors",
                column: "DID",
                principalTable: "Departments",
                principalColumn: "DID");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructors_Instructors_SupervisorId",
                table: "Instructors",
                column: "SupervisorId",
                principalTable: "Instructors",
                principalColumn: "InsID");

            migrationBuilder.AddForeignKey(
                name: "FK_InstructorSubjects_Instructors_InsID",
                table: "InstructorSubjects",
                column: "InsID",
                principalTable: "Instructors",
                principalColumn: "InsID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstructorSubjects_Subjects_SubID",
                table: "InstructorSubjects",
                column: "SubID",
                principalTable: "Subjects",
                principalColumn: "SubID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Instructors_SupervisorId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Departments_DID",
                table: "Instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_Instructors_Instructors_SupervisorId",
                table: "Instructors");

            migrationBuilder.DropForeignKey(
                name: "FK_InstructorSubjects_Instructors_InsID",
                table: "InstructorSubjects");

            migrationBuilder.DropForeignKey(
                name: "FK_InstructorSubjects_Subjects_SubID",
                table: "InstructorSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstructorSubjects",
                table: "InstructorSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Instructors",
                table: "Instructors");

            migrationBuilder.RenameTable(
                name: "InstructorSubjects",
                newName: "InstructorSubject");

            migrationBuilder.RenameTable(
                name: "Instructors",
                newName: "Instructor");

            migrationBuilder.RenameIndex(
                name: "IX_InstructorSubjects_InsID",
                table: "InstructorSubject",
                newName: "IX_InstructorSubject_InsID");

            migrationBuilder.RenameIndex(
                name: "IX_Instructors_SupervisorId",
                table: "Instructor",
                newName: "IX_Instructor_SupervisorId");

            migrationBuilder.RenameIndex(
                name: "IX_Instructors_DID",
                table: "Instructor",
                newName: "IX_Instructor_DID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstructorSubject",
                table: "InstructorSubject",
                columns: new[] { "SubID", "InsID" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Instructor",
                table: "Instructor",
                column: "InsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Instructor_SupervisorId",
                table: "Departments",
                column: "SupervisorId",
                principalTable: "Instructor",
                principalColumn: "InsID");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Departments_DID",
                table: "Instructor",
                column: "DID",
                principalTable: "Departments",
                principalColumn: "DID");

            migrationBuilder.AddForeignKey(
                name: "FK_Instructor_Instructor_SupervisorId",
                table: "Instructor",
                column: "SupervisorId",
                principalTable: "Instructor",
                principalColumn: "InsID");

            migrationBuilder.AddForeignKey(
                name: "FK_InstructorSubject_Instructor_InsID",
                table: "InstructorSubject",
                column: "InsID",
                principalTable: "Instructor",
                principalColumn: "InsID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InstructorSubject_Subjects_SubID",
                table: "InstructorSubject",
                column: "SubID",
                principalTable: "Subjects",
                principalColumn: "SubID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
