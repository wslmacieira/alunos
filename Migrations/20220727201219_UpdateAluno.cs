using Microsoft.EntityFrameworkCore.Migrations;

namespace alunos.Migrations
{
    public partial class UpdateAluno : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Alunos",
                table: "Alunos");

            migrationBuilder.RenameTable(
                name: "Alunos",
                newName: "tb_aluno");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "tb_aluno",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "tb_aluno",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "DataNascimento",
                table: "tb_aluno",
                newName: "data_nascimento");

            migrationBuilder.AlterColumn<string>(
                name: "nome",
                table: "tb_aluno",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_aluno",
                table: "tb_aluno",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_aluno",
                table: "tb_aluno");

            migrationBuilder.RenameTable(
                name: "tb_aluno",
                newName: "Alunos");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Alunos",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Alunos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "data_nascimento",
                table: "Alunos",
                newName: "DataNascimento");

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Alunos",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Alunos",
                table: "Alunos",
                column: "Id");
        }
    }
}
